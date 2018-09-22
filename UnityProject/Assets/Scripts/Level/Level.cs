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

    public void Unload()
    {
        EnemyFactory.Instance.ReleaseAllObjectsInPool();
        BulletFactory.Instance.ReleaseAllObjectsInPool();
        this.description = null;
        this.enemiesToSpawn = null;
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
            EnemyFactory.Instance.GetEnemy(enemyDescription.Type, enemyDescription.SpawnPosition);
            this.enemyStateArray[index] = EnemyState.Spawned;
        }
    }

    public bool IsFinished()
    {
        return levelTimer > description.Duration;
    }
}