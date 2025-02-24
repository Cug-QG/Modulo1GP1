using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RoundSystem/Stage", fileName = "StageData")]
public class Stage : ScriptableObject
{
    public List<Wave> waves;
    public float waveDelay = 2f; // Tempo di attesa tra una wave e l'altra (se attivo)
    public bool hasBoss = false;
    public GameObject bossPrefab;
}