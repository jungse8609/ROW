using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 5.0f;
    private float spawnTimer;

    private int waveCount = 0;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnMonsterWave();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnMonsterWave()
    {
        waveCount++;
        // foreach (var point in spawnPoints)
        // {
        //     Instantiate(monsterPrefab, point.position, Quaternion.identity);
        // }
        if (spawnPoints.Length >= 2)
        {
            int spawnIndex1 = Random.Range(0, spawnPoints.Length);
            int spawnIndex2;
            
            // 같은 인덱스가 선택되지 않도록 보장
            do
            {
                spawnIndex2 = Random.Range(0, spawnPoints.Length);
            } while (spawnIndex1 == spawnIndex2);

            // 두 위치에서 몬스터를 소환
            Instantiate(monsterPrefab, spawnPoints[spawnIndex1].position, Quaternion.identity);
            Instantiate(monsterPrefab, spawnPoints[spawnIndex2].position, Quaternion.identity);
        }

        if (waveCount % 5 == 0) // 5번째 웨이브마다 보스 소환
        {
            Instantiate(bossPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
