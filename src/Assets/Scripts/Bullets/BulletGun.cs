using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour {

    // Bullets Types
    public enum BULLETTYPE {SIMPLE, DIAGONALE, SPIRAL}; 

    // Bullets Prefabs
    public GameObject simpleBulletPrefab;
    public GameObject diagonalBulletPrefab;
    public GameObject spiralBulletPrefab;

    // Bullet variables
    [SerializeField] protected int numberOfSpiralBullets;
    [SerializeField] protected BULLETTYPE currentBulletType;
    protected Bullet currentBulletData;
    protected float cooldownChrono;

    // Fire Point Variables
    public GameObject firePoint;
    protected bool rotateFirePoint;

    // Energy variables
    public float energyMax;
    public float energy;
    public float energyRegenRate;
    public float costPerShot;
    protected bool canShoot;

    public void Fire()
    {
        if(cooldownChrono > currentBulletData.cooldown)
        {
            // All the work around checking the energy goes here.
            if(canShoot)
            {
                // We update the chronos
                cooldownChrono = 0f;

                // Standard Rotation
                Quaternion bulletRotation = Quaternion.identity;

                // We instantiate the new bullet(s)
                switch (currentBulletType)
                {
                    case BULLETTYPE.SIMPLE:
                        GameObject newBullet = (GameObject) Instantiate(simpleBulletPrefab, firePoint.transform.position, Quaternion.identity);
                        newBullet.GetComponent<SimpleBullet>().InitBullet(currentBulletData.damage, firePoint.transform.position,
                                                                          currentBulletData.speed, currentBulletData.bulletSide);
                        break;
                    case BULLETTYPE.DIAGONALE:
                        // Upper Bullet
                        bulletRotation.eulerAngles = new Vector3(0, 0, 45);
                        GameObject newUpperBullet = (GameObject) Instantiate(diagonalBulletPrefab, firePoint.transform.position, bulletRotation);
                        newUpperBullet.GetComponent<DiagonalBullet>().InitBullet(currentBulletData.damage,
                                                                                 firePoint.transform.position,
                                                                                 currentBulletData.speed,
                                                                                 currentBulletData.bulletSide);
                        // Lower Bullet
                        bulletRotation.eulerAngles = new Vector3(0, 0, -45);
                        GameObject newLowerBullet = (GameObject) Instantiate(diagonalBulletPrefab, transform.position, bulletRotation);
                        newLowerBullet.GetComponent<DiagonalBullet>().InitBullet(currentBulletData.damage,
                                                                                 firePoint.transform.position,
                                                                                 new Vector2(currentBulletData.speed.x, -currentBulletData.speed.y),
                                                                                 currentBulletData.bulletSide);
                        break;
                    case BULLETTYPE.SPIRAL:
                        // We instanciate as many bullets as asked by the editor.
                        float zFirePointRotation = firePoint.transform.eulerAngles.z;
                        for (int i = 0; i < numberOfSpiralBullets; i++)
                        {
                            bulletRotation.eulerAngles = new Vector3(0, 0, i * (360) / numberOfSpiralBullets + zFirePointRotation);
                            GameObject newSpiralBullet = (GameObject)Instantiate(spiralBulletPrefab, firePoint.transform.position, bulletRotation);
                            newSpiralBullet.GetComponent<SpiralBullet>().InitBullet(currentBulletData.damage,
                                                                                      firePoint.transform.position,
                                                                                      new Vector2(currentBulletData.speed.x, -currentBulletData.speed.y),
                                                                                      currentBulletData.bulletSide);
                        }
                        break;
                    default:
                        Debug.LogWarning("No bullet type selected. We can't instanciate a bullet.");
                        break;
                }

                // We take care of the energy
                energy -= costPerShot;
                canShoot = energy >= 0;
            }
        }
    }

    public void Start()
    {
        cooldownChrono = 0f;
        energy = energyMax;
        canShoot = true;
        rotateFirePoint = false;
        numberOfSpiralBullets = Mathf.Max(1, numberOfSpiralBullets);

        switch (currentBulletType)
        {
            case BULLETTYPE.SIMPLE:
                currentBulletData = simpleBulletPrefab.GetComponent<SimpleBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
            case BULLETTYPE.DIAGONALE:
                currentBulletData = diagonalBulletPrefab.GetComponent<DiagonalBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
            case BULLETTYPE.SPIRAL:
                currentBulletData = spiralBulletPrefab.GetComponent<SpiralBullet>();
                rotateFirePoint = true;
                firePoint.transform.rotation = Quaternion.identity;
                break;
            default:
                currentBulletType = BULLETTYPE.SIMPLE;
                currentBulletData = simpleBulletPrefab.GetComponent<SimpleBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
        }
    }

    public void Update()
    {
        cooldownChrono += Time.deltaTime;
        energy = Mathf.Min(energy + energyRegenRate * Time.deltaTime * ((canShoot) ? 1 : 0.75f), energyMax);

        if(rotateFirePoint)
        {
            firePoint.transform.Rotate(Vector3.forward, 1);
        }

        // In can the bullet gun cannot shoot due to a lack of energy, we check if it's recharged.
        if(!canShoot && energy == energyMax)
        {
            canShoot = true;
        }
    }

    public void BulletRotation()
    {
        cooldownChrono = 0f;
        switch (currentBulletType)
        {
            case BULLETTYPE.SIMPLE:
                currentBulletType = BULLETTYPE.DIAGONALE;
                currentBulletData = diagonalBulletPrefab.GetComponent<DiagonalBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
            case BULLETTYPE.DIAGONALE:
                currentBulletType = BULLETTYPE.SPIRAL;
                currentBulletData = spiralBulletPrefab.GetComponent<SpiralBullet>();
                rotateFirePoint = true;
                break;
            case BULLETTYPE.SPIRAL:
                currentBulletType = BULLETTYPE.SIMPLE;
                currentBulletData = simpleBulletPrefab.GetComponent<SimpleBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
            default:
                currentBulletType = BULLETTYPE.SIMPLE;
                currentBulletData = simpleBulletPrefab.GetComponent<SimpleBullet>();
                rotateFirePoint = false;
                firePoint.transform.rotation = Quaternion.identity;
                break;
        }
    }
}
