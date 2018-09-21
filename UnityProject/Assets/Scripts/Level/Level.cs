using System.Collections.Generic;
using UnityEngine;
using Data;

public class Level {

    private enum EnemyState { NotSpawn, Spawn};

    private LevelDescription description;
    private List<EnemyDescription> enemiesToSpawn;
    private EnemyState[] enemyStateArray;
    private float levelTimer;
    private float startTime;


    public void Load(LevelDescription levelDescription)
    {
        description = levelDescription;
        this.enemiesToSpawn = new List<EnemyDescription>(levelDescription.Enemies);
        this.startTime = Time.time;
        // Prepare a data structure to save which enemies you have.need to spawn using levelDescription.
        // Save the start time using Time.time.
    }

    public void Execute()
    {
        levelTimer = Time.time - this.startTime;
        for(int index = 0; index < this.enemiesToSpawn.Count; index++)
        {
            EnemyDescription enemyDescription = this.enemiesToSpawn[index];
            if(this.enemiesToSpawn[index].SpawnDate > levelTimer)
            {
                // Spawn
                EnemyFactory.Instance.GetEnemy(enemyDescription.Type, enemyDescription.SpawnPosition);
                this.enemiesToSpawn.RemoveAt(index);
                index--;
            }
        }
    }

    public bool IsFinished()
    {
        return false;
    }
}
