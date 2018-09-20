using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour {

    public GameObject simpleBulletPrefab;
    private Bullet currentBulletType;
    private float cooldownChrono;

    // Energy variables
    public float energyMax;
    public float energy;
    public float energyRegenRate;
    public float costPerShot;
    private bool canShoot;

    public void Fire()
    {
        if(cooldownChrono > currentBulletType.cooldown)
        {
            // All the work around checking the energy goes here.
            if(canShoot)
            {
                // We update the chronos
                cooldownChrono = 0f;

                // We instantiate the new bullet
                GameObject newBullet = (GameObject)Instantiate(simpleBulletPrefab, transform.position, Quaternion.identity);
                newBullet.GetComponent<SimpleBullet>().InitBullet(currentBulletType.damage, transform.position, 
                                                                  currentBulletType.speed, Bullet.BulletType.PLAYER);

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
        currentBulletType = simpleBulletPrefab.GetComponent<SimpleBullet>();
    }

    public void Update()
    {
        cooldownChrono += Time.deltaTime;
        energy = Mathf.Min(energy + energyRegenRate * Time.deltaTime * ((canShoot) ? 1 : 0.75f), energyMax);

        // In can the bullet gun cannot shoot due to a lack of energy, we check if it's recharged.
        if(!canShoot && energy == energyMax)
        {
            canShoot = true;
        }
    }
}
