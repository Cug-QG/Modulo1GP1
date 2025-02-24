using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RoundSystem/Wave", fileName = "WaveData")]
public class Wave : ScriptableObject
{
    [System.Serializable]
    public class EnemiesSpawnInfo
    {
        public GameObject enemyPrefab;
        public float number;
        public float spawnDelay = 0.5f; // Tempo tra ogni nemico della wave
    }

    public List<EnemiesSpawnInfo> enemies;
}