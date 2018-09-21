// <copyright file="GameManager.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    
    [SerializeField]
    private float rateOfEnemySpawn = 0.2f;

    [SerializeField]
    private float maximumRateOfEnemySpawn = 1f;

    [SerializeField]
    private float rateOfEnemySpawnIncreaseStep = 0.02f;
    
    private double lastEnemySpawnTime;

    public static GameManager Instance
    {
        get;
        private set;
    }
    
    public PlayerAvatar PlayerAvatar
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is multiple instance of singleton GameManager");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Spawn the player.
        GameObject player = (GameObject)GameObject.Instantiate(Instance.playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        this.PlayerAvatar = player.GetComponent<PlayerAvatar>();
        if (this.PlayerAvatar == null)
        {
            Debug.LogError("Can't retrieve the PlayerAvatar script.");
        }
    }
    
    private void Update()
    {
        this.RandomSpawn();
    }

    private void RandomSpawn()
    {
        if (this.rateOfEnemySpawn <= 0f)
        {
            return;
        }

        float durationBetweenTwoEnemySpawn = 1f / this.rateOfEnemySpawn;

        if (Time.time < this.lastEnemySpawnTime + durationBetweenTwoEnemySpawn)
        {
            // The bullet gun is in cooldown, it can't fire.
            return;
        }

        // Spawn an enemy.
        EnemyType enemyType = EnemyType.SimpleShotEnemy;
        if (Random.value < 0.2f)
        {
            enemyType = EnemyType.DiagonalShotEnemy;
        }

        float randomY = Random.Range(-4f, 4f);

        // Instantiate a new enemy.
        Vector2 position = new Vector3(10f, randomY);
        EnemyAvatar enemy = EnemyFactory.Instance.GetEnemy(enemyType, position);
        enemy.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

        /*
        GameObject gameObject = null;

        GameObject prefab = (GameObject)Resources.Load(prefabPath);
        gameObject = (GameObject)GameObject.Instantiate(prefab, position, Quaternion.Euler(0f, 0f, 180f));

        EnemyAvatar enemy = gameObject.GetComponent<EnemyAvatar>();
        enemy.PrefabPath = prefabPath;
        enemy.Position = position;
        */

        this.lastEnemySpawnTime = Time.time;

        // Up the difficulty.
        this.rateOfEnemySpawn += this.rateOfEnemySpawn > this.maximumRateOfEnemySpawn ? 0f : this.rateOfEnemySpawnIncreaseStep;
    }
}
