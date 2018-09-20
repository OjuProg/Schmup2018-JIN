using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : MonoBehaviour {

    public GameObject simpleBulletPrefab;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float cooldown;
    private float cooldownChrono;

    public void Fire()
    {
        if(cooldownChrono > cooldown)
        {
            cooldownChrono = 0f;
            GameObject newBullet = (GameObject) Instantiate(simpleBulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<SimpleBullet>().InitBullet(damage, transform.position, speed, Bullet.BulletType.PLAYER);
        }
    }

    public void Start()
    {
        cooldownChrono = 0f;
    }

    public void Update()
    {
        cooldownChrono += Time.deltaTime;
    }
}
