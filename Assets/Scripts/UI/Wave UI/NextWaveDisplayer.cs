using System.Collections.Generic;
using UnityEngine;

public class NextWaveDisplayer : MonoBehaviour
{
    public Spawner spawner;
    public WaveSystem waveSystem;
    public RectTransform informationPanel;
    public List<EnemySpawnInfoBox> spawnInfos;

    public void ShowNextWaveInformation()
    {
        DisableAllSpawnInfos();


        if (waveSystem.NextWave != null)
        {
            var enemies = waveSystem.NextWave.GetDiffrenceEnemies(spawner);

            if (enemies == null)
                return;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (i >= spawnInfos.Count)
                {
                    break;
                }

                spawnInfos[i].SetInfo(waveSystem.NextWave.GetEnemiesCount(enemies[i], spawner), enemies[i].name);
                spawnInfos[i].gameObject.SetActive(true);
            }
            informationPanel.gameObject.SetActive(true);
        }
    }

    private void DisableAllSpawnInfos()
    {
        foreach (var spawnInfo in spawnInfos)
        {
            spawnInfo.gameObject.SetActive(false);
        }
    }

    public void HideNextWaveInformation()
    {
        informationPanel.gameObject.SetActive(false);
    }
}
