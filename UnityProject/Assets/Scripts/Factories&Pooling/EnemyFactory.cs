using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

    // Bullet pools
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Singleton
    public static EnemyFactory Instance { get; private set; }

    void Awake() {
        // Creating the Singleton
        Debug.Assert(EnemyFactory.Instance == null);
        EnemyFactory.Instance = this;

        // Creation of the pools
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
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
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public EnemyAvatar GetEnemy(EnemyType enemyType, Vector2 initialPosition)
    {
        GameObject gameObject;
        switch (enemyType)
        {
            case EnemyType.SimpleShotEnemy:
                gameObject = EnemyFactory.Instance.SpawnFromPool("SimpleShotEnemy");
                break;
            case EnemyType.DiagonalShotEnemy:
                gameObject = EnemyFactory.Instance.SpawnFromPool("DiagonalShotEnemy");
                break;
            default:
                throw new System.Exception("Unknown enemy type." + enemyType);
        }

        EnemyAvatar enemy = gameObject.GetComponent<EnemyAvatar>();
        enemy.Position = initialPosition;
        return enemy;
    }

    public void Release(EnemyAvatar enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
