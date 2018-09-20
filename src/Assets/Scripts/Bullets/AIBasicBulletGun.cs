using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : MonoBehaviour {

    public GameObject EnemyBulletPrefab;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float cooldown;
    private float cooldownChrono;

    public void Start()
    {
        cooldownChrono = 0f;
    }

    public void Update()
    {
        cooldownChrono += Time.deltaTime;
        if (cooldownChrono > cooldown)
        {
            cooldownChrono = 0f;
            GameObject newBullet = (GameObject)Instantiate(EnemyBulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<SimpleBullet>().InitBullet(damage, transform.position, speed, Bullet.BulletType.ENEMY);
        }
    }
}
