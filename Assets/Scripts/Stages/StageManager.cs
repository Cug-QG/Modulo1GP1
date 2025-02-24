using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Wave;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public static StageManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    public List<Stage> stages; // Lista di round nel gioco
    public Transform spawnParent; // Dove spawnare i nemici
    private int currentRoundIndex = 0;
    public List<GameObject> livingEnemies;

    [SerializeField] Transform minYspawnPosition;
    [SerializeField] Transform maxYspawnPosition;
    [SerializeField] Transform bossTargetPosition;
    [SerializeField] float bossSpeed;

    void Start()
    {
        StartCoroutine(StartRounds());
    }

    IEnumerator StartRounds()
    {
        foreach (Stage stage in stages)
        {
            yield return StartCoroutine(SpawnRound(stage));
        }
    }

    IEnumerator SpawnRound(Stage round)
    {
        UIManager.Instance.SetMessagesText("Stage: " + (currentRoundIndex + 1));

        foreach (Wave wave in round.waves)
        {
            yield return StartCoroutine(SpawnWave(wave));

            yield return new WaitForSeconds(round.waveDelay);
        }

        if (round.hasBoss)
        {
            while (livingEnemies.Count != 0)
            {
                yield return null;
            }
            UIManager.Instance.SetMessagesText("Spawn del Boss!");
            StartCoroutine(BossStage(Instantiate(round.bossPrefab, spawnParent.position, Quaternion.identity, spawnParent)));
        }

        currentRoundIndex++;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemies in wave.enemies)
        {
            StartCoroutine(SpawnEnemies(enemies));
        }
        while (livingEnemies.Count!=0)
        {
            yield return null;
        }
    }

    IEnumerator SpawnEnemies(EnemiesSpawnInfo enemies)
    {
        for (int i = 0; i < enemies.number; i++)
        {
            GameObject enemy = Instantiate(enemies.enemyPrefab, spawnParent.position, Quaternion.identity, spawnParent);
            livingEnemies.Add(enemy);
            float randomY = Random.Range(minYspawnPosition.position.y, maxYspawnPosition.position.y);
            Vector2 position = new Vector2(spawnParent.position.x, randomY);
            enemy.transform.position = position;
            yield return new WaitForSeconds(enemies.spawnDelay);
        }
    }

    IEnumerator BossStage(GameObject boss)
    {
        StartCoroutine(MoveBoss(boss));
        while (boss != null)
        {
            yield return null;
        }
        UIManager.Instance.ToggleWinningMenu(true);
    }

    IEnumerator MoveBoss(GameObject boss)
    {
        while (boss.transform.position != bossTargetPosition.position)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, bossTargetPosition.position, bossSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
