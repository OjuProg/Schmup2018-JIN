// <copyright file="GameManager.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Data;

public class GameManager : MonoBehaviour
{
    private enum GameState { Play, Pause, GameOver };
    public enum GameType { Levels, Endless };

    [SerializeField]
    private GUIManager guiManager;

    [SerializeField]
    private TextAsset levelDescriptionXml;

    private List<LevelDescription> levelDescriptions;
    private LevelDescription currentLevelDescription;

    private int currentLevelIndex = -1;

    private GameState gameState;

    [SerializeField]
    private GameType gameType;

    public GameType CurrentGameType
    {
        get
        {
            return gameType;
        }

        set
        {
            if(value.GetType() == typeof(GameType))
            {
                this.gameType = value;
            }
        }
    }

    [SerializeField]
    private Level currentLevel;

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

    public void OnPause()
    {
        guiManager.TogglePauseMenu();
        gameState = (gameState == GameState.Pause) ? GameState.Play : GameState.Pause;

        switch (gameState)
        {
            case GameState.Play:
                Time.timeScale = 1;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                break;
            default:
                Debug.LogWarning("Gamestate type." + gameState.ToString() + " not registered.");
                break;
        }
    }

    public void OnGameOver()
    {
        guiManager.ToggleGameOverMenu();

        gameState = GameState.GameOver;
        Time.timeScale = 0;
    }

    private void OnAvatarDeath(BaseAvatar baseAvatar)
    {
        if (baseAvatar.GetType() == typeof(PlayerAvatar))
        {
            OnGameOver();
        }
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

    public void OnEnable()
    {
        BaseAvatar.OnDeath += OnAvatarDeath;
    }

    public void OnDisable()
    {
        BaseAvatar.OnDeath -= OnAvatarDeath;
    }

    private void Start()
    {
        gameState = GameState.Play;
        Time.timeScale = 1;

        this.levelDescriptions = XmlHelpers.DeserializeDatabaseFromXML<LevelDescription>(this.levelDescriptionXml);

        // Spawn the player.
        GameObject player = (GameObject)GameObject.Instantiate(Instance.playerPrefab, new Vector3(0f, 0f), Quaternion.identity);
        this.PlayerAvatar = player.GetComponent<PlayerAvatar>();
        if (this.PlayerAvatar == null)
        {
            Debug.LogError("Can't retrieve the PlayerAvatar script.");
        }
        
        switch (gameType)
        {
            case GameType.Levels:
                currentLevel = new Level();
                StarNextLevel();
                break;
            case GameType.Endless:
                break;
            default:
                Debug.LogWarning("Unknown game type." + gameType.ToString());
                break;
        }
    }
    
    private void StarNextLevel()
    {
        if(currentLevel != null)
        {
            currentLevel.Unload();
        }

        this.currentLevelIndex++;

        if(this.currentLevelIndex >= this.levelDescriptions.Count)
        {
            OnGameOver();
            return;
        }

        currentLevelDescription = this.levelDescriptions[currentLevelIndex];
        currentLevel = new Level();
        currentLevel.Load(currentLevelDescription);
    }

    private void Update()
    {
        switch(gameType)
        {
            case GameType.Levels:
                if (gameState == GameState.Play)
                {
                    this.currentLevel.Execute();
                    if (this.currentLevel.IsFinished())
                    {
                        this.StarNextLevel();
                    }
                }
                break;

            case GameType.Endless:
                this.RandomSpawn();
                break;
            default:
                Debug.LogWarning("Unknown game type." + gameType.ToString());
                break;
        }
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
        if (UnityEngine.Random.value < 0.2f)
        {
            enemyType = EnemyType.DiagonalShotEnemy;
        }

        float randomY = UnityEngine.Random.Range(-4f, 4f);

        // Instantiate a new enemy.
        Vector2 position = new Vector3(10f, randomY);
        EnemyAvatar enemy = EnemyFactory.Instance.GetEnemy(enemyType, position);
        enemy.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

        this.lastEnemySpawnTime = Time.time;

        // Up the difficulty.
        this.rateOfEnemySpawn += this.rateOfEnemySpawn > this.maximumRateOfEnemySpawn ? 0f : this.rateOfEnemySpawnIncreaseStep;
    }

}
