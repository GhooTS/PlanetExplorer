using GTVariable;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;



[RequireComponent(typeof(WaveSpawnRunner))]
public class WaveSystem : MonoBehaviour
{
    [HideInInspector]
    public WaveData[] waves;


    //Data
    [SerializeField] private IntVariable currentWaveNumber;
    [SerializeField] private FloatVariable nextWaveTime;
    [SerializeField] private IntVariable aliveEnemies;

    //Properties
    public int CurrentWaveNumber { get { return CurrentWaveIndex + 1; } }
    public int CurrentWaveIndex { get; private set; } = -1;
    public int MaxWaves { get { return waves.Length; } }
    public int AliveEnemies { get { return aliveEnemies.value; } }
    public int EnemiesInCurrentWave
    {
        get
        {
            return CurrentWaveIndex > waves.Length || CurrentWaveIndex < 0 ? -1 : waves[CurrentWaveIndex].GetEnemiesCount();
        }
    }
    public bool WaveActive { get; private set; } = false;
    public bool LastWave
    {
        get
        {
            return CurrentWaveNumber == waves.Length;
        }
    }
    public float NextWaveStartTime
    {
        get
        {
            return autoWavePooling ? nextWaveTime.value : -1;
        }
    }
    public WaveData CurrentWave
    {
        get
        {
            return IsValideIndexForCurrentWave() ? waves[CurrentWaveIndex] : null;
        }
    }
    public WaveData NextWave
    {
        get
        {
            return IsValideIndexForNextWave() ? waves[CurrentWaveIndex + 1] : null;
        }
    }


    //Options
    public bool autoWaveStart = true;
    public bool autoWavePooling = true;
    public bool waitForEnemiesDead = true;
    [Min(0)]
    public float waveDuration = 90;

    //Events
    public UnityEvent waveRushed;
    public UnityEvent waveStarted;
    public UnityEvent waveEnded;
    public UnityEvent lastWaveStarted;
    public UnityEvent allWavesCleared;

    //private fields
    private WaveSpawnRunner waveSpawner;
    private Coroutine nextWaveAutoStart;
    private Coroutine waveRunner;


    private void Start()
    {
        waveSpawner = GetComponent<WaveSpawnRunner>();
        CurrentWaveIndex = -1;
        aliveEnemies.value = 0;
        if (autoWaveStart)
        {
            StartNextWave();
        }
    }


    public void StartNextWave()
    {
        MoveToNextWave();

        if (IsValideIndexForCurrentWave())
        {
            StopCurrentWaveRunner();

            StartWaveDurationCounter();

            SetEnemiesLeft();

            waveSpawner.StartSpawing(CurrentWave);
            waveRunner = StartCoroutine(WaveRunner());
            waveStarted?.Invoke();
            WaveActive = true;
        }


    }


    public void MoveToNextWave()
    {
        if (LastWave)
            return;

        CurrentWaveIndex++;
        currentWaveNumber.value = CurrentWaveNumber;
    }

    public void MoveToPrevWave()
    {
        if (CurrentWaveIndex == 0)
            return;

        CurrentWaveIndex--;
        currentWaveNumber.value = CurrentWaveNumber;
    }

    public void RushWave()
    {
        if (WaveActive)
        {
            waveRushed?.Invoke();
        }

        StartNextWave();
    }

    public void EndWave()
    {
        WaveActive = false;
        waveEnded?.Invoke();

        if (LastWave)
        {
            allWavesCleared?.Invoke();
            return;
        }
    }

    public float GetAllWavesDuration()
    {
        float output = 0;

        foreach (var wave in waves)
        {
            output += wave.GetSpawnEndTime();
        }

        return output;
    }

    public int GetEnemiesCountInAllWaves()
    {
        int output = 0;

        foreach (var wave in waves)
        {
            output += wave.GetEnemiesCount();
        }

        return output;
    }

    private bool IsValideIndexForCurrentWave()
    {
        return CurrentWaveIndex >= 0 && CurrentWaveIndex < waves.Length;
    }

    private bool IsValideIndexForNextWave()
    {
        return CurrentWaveIndex >= -1 && LastWave == false;
    }

    private void StopCurrentWaveRunner()
    {
        if (waveRunner != null)
        {
            StopCoroutine(waveRunner);
        }
    }

    private void StartWaveDurationCounter()
    {
        if (LastWave == false && autoWavePooling)
        {
            if (nextWaveAutoStart != null)
            {
                StopCoroutine(nextWaveAutoStart);
            }
            nextWaveAutoStart = StartCoroutine(CountTimeToWaveEnd());
        }
    }

    private void SetEnemiesLeft()
    {
        if (aliveEnemies.value > 0)
        {
            aliveEnemies.value += CurrentWave.GetEnemiesCount();
        }
        else
        {
            aliveEnemies.value = CurrentWave.GetEnemiesCount();
        }
    }

    private IEnumerator WaveRunner()
    {


        while (aliveEnemies.value > 0)
        {
            yield return null;
        }

        EndWave();
    }

    private IEnumerator CountTimeToWaveEnd()
    {
        nextWaveTime.value = waveDuration;
        while (true)
        {
            yield return null;
            nextWaveTime.value -= Time.deltaTime;
            if (nextWaveTime.value < 0)
            {
                waveEnded?.Invoke();
                StartNextWave();
            }
        }
    }


}
