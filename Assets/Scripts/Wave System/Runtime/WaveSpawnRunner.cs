using System.Collections;
using UnityEngine;



//public class EnemyTracer : MonoBehaviour
//{
//    public WaveEnemyTracker WaveEnemyTracker { get; set; }
//    public int WaveIndex { get; set; }

//    private void OnDisable()
//    {
//        WaveEnemyTracker.RemoveEnemey(this);
//    }

//}

//public class WaveEnemyTracker
//{
//    private List<EnemyTracer> enemies = new List<EnemyTracer>();
//    public int GetEnemyCount { get { return enemies.Count; } }

//    public void AddEnemy(EnemyTracer enemy)
//    {
//        enemies.Add(enemy);
//    }

//    public void RemoveEnemey(EnemyTracer enemy)
//    {
//        enemies.Remove(enemy);
//    }


//}

public class WaveSpawnRunner : MonoBehaviour
{
    public void StartSpawing(WaveData wave)
    {
        wave.ManageRelation();
        foreach (var spawnData in wave.spawnDatas)
        {
            if (spawnData.prefab != null && spawnData.spawnPoint != null)
            {
                StartCoroutine(SpawnEnemy(spawnData));
            }
            else
            {
                Debug.LogWarning($"prefab or spawn point was null for '{spawnData.name}' in wave '{wave.waveName}'");
            }
        }
    }

    public IEnumerator SpawnEnemy(SpawnData spawnData)
    {

        yield return new WaitForSeconds(spawnData.GetSpawnStartTime());

        WaitForSeconds waiter = new WaitForSeconds(spawnData.interval);

        for (int i = 0; i < spawnData.amount; i++)
        {
            spawnData.spawnPoint.Spawn(spawnData.prefab);
            yield return waiter;
        }

    }

    public void StopWave()
    {
        //logic 
    }
}
