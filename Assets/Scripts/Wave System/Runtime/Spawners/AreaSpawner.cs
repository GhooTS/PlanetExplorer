using UnityEngine;

public class AreaSpawner : Spawner
{
    public Vector3 spawnSize = Vector3.one;
    public bool roundPosition = true;
    public float roundTo = 0.5f;

    protected override void SpawnObject(GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, GetRandomPositionWithInSpawnArea(), Quaternion.identity);
    }

    private Vector3 GetRandomPositionWithInSpawnArea()
    {
        Vector3 output = transform.position;

        output.x += GetRandomFromHalfRange(spawnSize.x);
        output.y += GetRandomFromHalfRange(spawnSize.y);
        output.z += GetRandomFromHalfRange(spawnSize.z);

        return output;
    }

    private float GetRandomFromHalfRange(float range)
    {
        float half = range / 2;
        return UnityEngine.Random.Range(-half, half);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, spawnSize);
    }
}