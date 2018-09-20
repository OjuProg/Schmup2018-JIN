using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    public enum BulletType {PLAYER, ENEMY};

    public int damage;
    public float cooldown;
    public Vector2 speed;
    public Vector2 position;
    public BulletType bulletType;

    public virtual void InitBullet(int damageValue, Vector2 bulletPosition, Vector2 bulletSpeed, BulletType newBulletType)
    {
        damage = damageValue;
        position = bulletPosition;
        speed = bulletSpeed;
        bulletType = newBulletType;
    }

    public virtual void UpdatePosition()
    {
        position += speed * Time.deltaTime;
        transform.position = position;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        UpdatePosition();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BaseAvatar avatar = collision.GetComponent<BaseAvatar>();
        if(avatar != null)
        {
            if(bulletType == BulletType.ENEMY && avatar.GetType() == typeof(PlayerAvatar) ||
               bulletType == BulletType.PLAYER && avatar.GetType() == typeof(EnemyAvatar))
            {
                avatar.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
