using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string waveName = "Wave";
    public SpawnData[] spawnDatas;


    public List<Spawner> GetDiffrenceSpawners()
    {
        if (spawnDatas.Length == 0)
            return null;

        List<Spawner> output = new List<Spawner>();

        foreach (var data in spawnDatas)
        {
            if (data.SpawnPoint == null || output.Contains(data.SpawnPoint))
                continue;

            output.Add(data.SpawnPoint);
        }

        return output;
    }

    public List<Spawner> GetDiffrenceSpawners(GameObject prefab)
    {
        if (spawnDatas.Length == 0)
            return null;

        List<Spawner> output = new List<Spawner>();

        foreach (var data in spawnDatas)
        {
            if (data.SpawnPoint == null || output.Contains(data.SpawnPoint))
                continue;

            if (data.prefab == null || data.prefab != prefab)
                continue;

            output.Add(data.SpawnPoint);
        }


        return output;
    }

    public List<GameObject> GetDiffrenceEnemies()
    {
        if (spawnDatas.Length == 0)
            return null;

        List<GameObject> output = new List<GameObject>();

        foreach (var data in spawnDatas)
        {
            if (data.prefab == null || output.Contains(data.prefab))
                continue;

            output.Add(data.prefab);
        }

        return output;
    }

    public List<GameObject> GetDiffrenceEnemies(Spawner spawner)
    {
        if (spawnDatas.Length == 0)
            return null;

        List<GameObject> output = new List<GameObject>();

        foreach (var data in spawnDatas)
        {
            if (data.prefab == null || output.Contains(data.prefab))
            {
                continue;
            }

            if (data.SpawnPoint == null || data.SpawnPoint != spawner)
            {
                continue;
            }

            output.Add(data.prefab);
        }

        return output;
    }


    public int GetEnemiesCount()
    {
        int output = 0;

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            output += spawnDatas[i].amount;
        }

        return output;
    }

    public int GetEnemiesCount(GameObject enemyPrefab)
    {
        int output = 0;

        foreach (var data in spawnDatas)
        {
            if (data.prefab == enemyPrefab)
            {
                output += data.amount;
            }
        }


        return output;
    }

    public int GetEnemiesCount(Spawner spawner)
    {
        int output = 0;

        foreach (var data in spawnDatas)
        {
            if (data.SpawnPoint == spawner)
            {
                output += data.amount;
            }
        }


        return output;
    }

    public int GetEnemiesCount(GameObject enemyPrefab, Spawner spawner)
    {
        int output = 0;

        foreach (var data in spawnDatas)
        {
            if (data.prefab == enemyPrefab && data.SpawnPoint == spawner)
            {
                output += data.amount;
            }
        }


        return output;
    }

    public float GetWaveSpawnStartTime()
    {
        float output = Mathf.Infinity;

        foreach (var data in spawnDatas)
        {
            if (output > data.GetSpawnStartTime())
            {
                output = data.GetSpawnStartTime();
            }
        }

        return output;
    }

    public float GetSpawnEndTime()
    {
        float output = 0;

        foreach (var data in spawnDatas)
        {
            if (output < data.GetSpawnEndTime())
            {
                output = data.GetSpawnEndTime();
            }
        }

        return output;
    }

    public void ManageRelation()
    {
        foreach (var data in spawnDatas)
        {
            data.relationDelay = 0;
            if (data.relationIndex > -1)
            {
                switch (data.relation)
                {
                    case SpawnDataRelation.after:
                        data.relationDelay = spawnDatas[data.relationIndex].GetSpawnEndTime();
                        break;
                    case SpawnDataRelation.sameAs:
                        data.relationDelay = spawnDatas[data.relationIndex].GetSpawnStartTime();
                        break;
                }
            }
        }

    }
}
