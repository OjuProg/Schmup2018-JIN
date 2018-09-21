using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour {

    // Bullet Prefabs
    [SerializeField]
    private GameObject playerBulletPrefab;
    [SerializeField]
    private GameObject enemyBulletPrefab;
    [SerializeField]
    private GameObject playerSpiralBulletPrefab;

    // Bullet pools
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    // Singleton
    public static BulletFactory Instance { get; private set; }

    private void Awake()
    {
        // Creating the Singleton
        Debug.Assert(BulletFactory.Instance == null);
        BulletFactory.Instance = this;

        // Creation of the pools
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public Bullet GetBullet(BulletType bulletType, Vector2 initialPosition)
    {
        GameObject gameObject;
        switch(bulletType)
        {
            case BulletType.PlayerBullet:
                gameObject = BulletFactory.Instance.SpawnFromPool("PlayerBullets");
                break;
            case BulletType.EnemyBullet:
                gameObject = BulletFactory.Instance.SpawnFromPool("EnemyBullets");
                break;
            case BulletType.PlayerSpiralBullet:
                gameObject = BulletFactory.Instance.SpawnFromPool("PlayerSpiralBullets");
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
        bullet.gameObject.SetActive(false);
    }
}
