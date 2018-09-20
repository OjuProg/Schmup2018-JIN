using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    public enum BULLETSIDE{PLAYER, ENEMY};

    public int damage;
    public float cooldown;
    public Vector2 speed;
    public Vector2 position;
    public BULLETSIDE bulletSide;

    public virtual void InitBullet(int damageValue, Vector2 bulletPosition, Vector2 bulletSpeed, BULLETSIDE newBulletSide)
    {
        damage = damageValue;
        position = bulletPosition;
        speed = bulletSpeed;
        bulletSide = newBulletSide;
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
            if(bulletSide == BULLETSIDE.ENEMY && avatar.GetType() == typeof(PlayerAvatar) ||
               bulletSide == BULLETSIDE.PLAYER && avatar.GetType() == typeof(EnemyAvatar))
            {
                avatar.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
