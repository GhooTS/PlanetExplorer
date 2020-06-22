using UnityEngine;

public enum SpawnDataRelation
{
    none, after, sameAs
}

[System.Serializable]
public class SpawnData
{
    public string name = "Spawn Data";
    public GameObject prefab;
    public Spawner spawnPoint;
    public Spawner SpawnPoint { get { return spawnPoint; } }
    [Min(0)]
    public int amount;
    [Min(0)]
    public float interval;
    [Min(0)]
    public float delay = 0;
    [HideInInspector]
    public float relationDelay = 0;

    public SpawnDataRelation relation;
    public int relationIndex = -1;


    public float GetSpawnTime()
    {
        return amount * interval;
    }

    public float GetSpawnEndTime()
    {
        return GetSpawnStartTime() + GetSpawnTime();
    }

    public float GetSpawnStartTime()
    {
        return delay + relationDelay;
    }
}
