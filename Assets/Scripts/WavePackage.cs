using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavePackage
{
    [SerializeField]
    public WaveEnemy[] WaveEnemies;
    [SerializeField]
    public float Delay;


    public List<GameObject> ShuffledEnemies;

    public void ShuffleWave()
    {
        ShuffledEnemies = new List<GameObject>();

        foreach (WaveEnemy waveEnemy in WaveEnemies)
        {
            for (int i = 0; i < waveEnemy.EnemyAmount; i++)
            {
                int randomIndex = Random.Range(0, ShuffledEnemies.Count - 1);
                ShuffledEnemies.Insert(randomIndex, waveEnemy.EnemyPrefab);
            }
        }
    }
}
