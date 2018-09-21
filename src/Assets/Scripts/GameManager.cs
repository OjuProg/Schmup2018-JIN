using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public UIManager uiManager;

    public bool pause;
    public float enemySpawnCooldown;


    public void OnEnable()
    {
        BaseAvatar.OnDeath += StopGame;
    }

    public void OnDisable()
    {
        BaseAvatar.OnDeath -= StopGame;
    }

    public void StopGame(BaseAvatar baseAvatar)
    {
        if (baseAvatar.GetType() == typeof(PlayerAvatar))
        {
            Time.timeScale = 0;
            uiManager.ToggleGameOverMenu();
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        pause = false;
        Time.timeScale = 1;
        Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        InvokeRepeating("InvokeEnemy", 3f, 3f);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void InvokeEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(9, Random.Range(-3f, 3f), 0), Quaternion.identity);
        EnemyAvatar enemyAvatar = enemy.GetComponent<EnemyAvatar>();
        enemyAvatar.maxSpeed = Random.Range(0.5f, 4f);
        AIBasicBulletGun aiBulletGun = enemy.GetComponent<AIBasicBulletGun>();
        if(aiBulletGun != null)
        {
            aiBulletGun.RandomizeEnemyShootingBehavior(enemyAvatar.maxSpeed);
        }
    }

    public void OnPause()
    {
        uiManager.TogglePauseMenu();
        pause = !pause;
        if(!pause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
