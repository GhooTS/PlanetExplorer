using UnityEngine;

public class PointSpawner : Spawner
{
    protected override void SpawnObject(GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}

