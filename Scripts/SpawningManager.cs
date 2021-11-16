using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public static SpawningManager shared;

    public WaveLevel WavesLevel;

    [Header("Scene setup")]
    public Transform StartPoint;
    public Transform EnemiesHolder;


    public List<GameObject> EnemiesAlive = new List<GameObject>();

    public bool AllSpawned;
    bool alreadyWon;

    void Awake()
    {
        if (shared == null)
            shared = this;
    }


    void Start()
    {
        AllSpawned = false;
        StartCoroutine(Spawning());
    }

    void Update()
    {
        if (AllSpawned)
            CheckWin();
    }

    void CheckWin()
    {
        if (PlayerData.shared.Health > 0 && EnemiesAlive.Count == 0 && !alreadyWon)
            WinLevel();
    }

    void WinLevel()
    {
        alreadyWon = true;

        MySceneManager.shared?.SaveProgress();
        WinScreenLogic.shared?.EnableWinScreen();
    }


    IEnumerator Spawning()
    {
        WavesLevel.ShuffleWaves();

        yield return new WaitForSeconds(WavesLevel.WaveDelay);

        int waveIndex = 0;

        while (waveIndex < WavesLevel.Waves.Length)
        {
            WavePackage wave = WavesLevel.Waves[waveIndex];

            yield return StartCoroutine(SpawnWave(wave));

            while (EnemiesAlive.Count > 0)
            {
                yield return null;
            }

            waveIndex += 1;
            yield return new WaitForSeconds(WavesLevel.WaveDelay);
        }

        AllSpawned = true;
    }

    IEnumerator SpawnWave(WavePackage wave)
    {
        int enemyIndex = 0;

        while (enemyIndex < wave.ShuffledEnemies.Count)
        {
            GameObject enemy = wave.ShuffledEnemies[enemyIndex];

            SpawnOneEnemy(enemy);

            enemyIndex += 1;
            yield return new WaitForSeconds(wave.Delay);
        }
    }




    void SpawnOneEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, StartPoint.position, Quaternion.identity, EnemiesHolder);
        EnemiesAlive.Add(enemy);
    }
}
