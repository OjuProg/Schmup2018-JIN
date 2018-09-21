using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour {

    [SerializeField]
    private GameObject playerBulletPrefab;
    [SerializeField]
    private GameObject enemyBulletPrefab;
    [SerializeField]
    private GameObject playerSpiralBulletPrefab;

    public static BulletFactory Instance { get; private set; }

    private void Awake()
    {
        Debug.Assert(BulletFactory.Instance == null);
        BulletFactory.Instance = this;
    }

    public Bullet GetBullet(BulletType bulletType, Vector2 initialPosition)
    {
        GameObject gameObject;
        switch(bulletType)
        {
            case BulletType.PlayerBullet:
                gameObject = (GameObject)GameObject.Instantiate(this.playerBulletPrefab);
                break;
            case BulletType.EnemyBullet:
                gameObject = (GameObject)GameObject.Instantiate(this.enemyBulletPrefab);
                break;
            case BulletType.PlayerSpiralBullet:
                gameObject = (GameObject)GameObject.Instantiate(this.playerSpiralBulletPrefab);
                break;
            default:
                throw new System.Exception("Unknown bullet type." + bulletType);
        }

        Bullet bullet = gameObject.GetComponent<Bullet>();
        bullet.Position = initialPosition;
        return bullet;
    }

    public void Release(Bullet bullet)
    {
        GameObject.Destroy(bullet.gameObject);
    }
}
