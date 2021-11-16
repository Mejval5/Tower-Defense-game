using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datascripts/WaveLevel")]
public class WaveLevel : ScriptableObject
{
    public WavePackage[] Waves;
    public int WaveDelay;

    public void ShuffleWaves()
    {
        foreach (WavePackage wave in Waves)
        {
            wave.ShuffleWave();
        }
    }
}
