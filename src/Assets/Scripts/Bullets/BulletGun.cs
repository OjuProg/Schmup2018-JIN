using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletGun : MonoBehaviour {

    public GameObject simpleBulletPrefab;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float cooldown;
    private float cooldownChrono;

    // Energy variables
    public float energyMax;
    public float energy;
    public float energyRegenRate;
    public float costPerShot;
    private bool canShoot;

    public void Fire()
    {
        if(cooldownChrono > cooldown)
        {
            // All the work around checking the energy goes here.
            if(canShoot)
            {
                // We update the chronos
                cooldownChrono = 0f;

                // We instantiate the new bullet
                GameObject newBullet = (GameObject)Instantiate(simpleBulletPrefab, transform.position, Quaternion.identity);
                newBullet.GetComponent<SimpleBullet>().InitBullet(damage, transform.position, speed, Bullet.BulletType.PLAYER);

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
