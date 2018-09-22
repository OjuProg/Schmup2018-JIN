using System.Collections.Generic;
using UnityEngine;
using Data;

public class Level {

    private enum EnemyState { NotSpawned, Spawned };

    private LevelDescription description;
    private List<EnemyDescription> enemiesToSpawn;
    private EnemyState[] enemyStateArray;

    private float startTime;
    private float levelTimer;

    public void Load(LevelDescription levelDescription)
    {
        description = levelDescription;
        this.enemiesToSpawn = new List<EnemyDescription>(levelDescription.Enemies);
        this.startTime = Time.time;

        // Enemies
        this.enemyStateArray = new EnemyState[(this.description.Enemies != null) ? this.description.Enemies.Length : 0];
        for (int index = 0; index < this.enemyStateArray.Length; index++)
        {
            this.enemyStateArray[index] = EnemyState.NotSpawned;
        }
    }


    public void Execute()
    {
        levelTimer = Time.time - this.startTime;

        for(int index = 0; index < this.enemiesToSpawn.Count; index++)
        {
            EnemyDescription enemyDescription = this.description.Enemies[index];
            if (this.enemyStateArray[index] == EnemyState.Spawned)
            {
                continue;
            }

            if (levelTimer < enemyDescription.SpawnDate)
            {
                continue;
            }

            // Spawn
            Debug.Log("Enemy Spawned!");
            EnemyFactory.Instance.GetEnemy(enemyDescription.Type, enemyDescription.SpawnPosition);
            this.enemyStateArray[index] = EnemyState.Spawned;
        }
    }

    public bool IsFinished()
    {
        return levelTimer > description.Duration;
    }
}

/*
        for(int index = 0; index < this.enemiesToSpawn.Count; index++)
        {
            EnemyDescription enemyDescription = this.enemiesToSpawn[index];
            if(this.enemiesToSpawn[index].SpawnDate > levelTimer)
            {
               
                this.enemiesToSpawn.RemoveAt(index);
                index--;
            }
        }
*/