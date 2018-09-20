using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : MonoBehaviour {

    public GameObject EnemyBulletPrefab;
    private Bullet currentBulletType;
    private float cooldownChrono;

    public void Start()
    {
        cooldownChrono = 0f;
        currentBulletType = EnemyBulletPrefab.GetComponent<SimpleBullet>();
    }

    public void Update()
    {
        cooldownChrono += Time.deltaTime;
        if (cooldownChrono > currentBulletType.cooldown)
        {
            cooldownChrono = 0f;
            GameObject newBullet = (GameObject)Instantiate(EnemyBulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<SimpleBullet>().InitBullet(currentBulletType.damage, transform.position, 
                                                              currentBulletType.speed, Bullet.BulletType.ENEMY);
        }
    }
}
