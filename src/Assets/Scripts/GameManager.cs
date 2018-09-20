using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public float enemySpawnCooldown;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        GameObject player = (GameObject) Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        InvokeRepeating("InvokeEnemy", 3f, 3f);
    }

    public void InvokeEnemy()
    {
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, new Vector3(9, Random.Range(-3f, 3f), 0), Quaternion.identity);
    }
}
