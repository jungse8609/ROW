using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 5.0f; // 몬스터 스폰 간격 (초단위)
    private float spawnTimer;

    private int waveCount = 0;
    public int mspawncount = 2; // 한 웨이브당 소환할 몬스터 수 (x값)

    private void Update()
    {
        spawnTimer -= Time.deltaTime; // 매 프레임마다 타이머 감소

        if (spawnTimer <= 0)
        {
            SpawnMonsterWave();
            spawnTimer = spawnInterval;
            if (waveCount % 3 == 0){
                mspawncount ++;
            }
        }
    }

    private void SpawnMonsterWave()
    {
        waveCount++;
        
        if (spawnPoints.Length > 0)
        {
            // 스폰 포인트 중복을 방지하기 위해 인덱스를 섞음
            int[] shuffledIndices = ShuffleIndices(spawnPoints.Length);

            // 최대 x마리의 몬스터를 소환 (스폰포인트가 부족하면 스폰포인트 개수만큼만 소환)
            int spawnCount = Mathf.Min(mspawncount, spawnPoints.Length);
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(monsterPrefab, spawnPoints[shuffledIndices[i]].position, Quaternion.identity);
            }
        }

        if (waveCount % 5 == 0) // 5 웨이브마다 보스 생성
        {
            Instantiate(bossPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }

    // 스폰 포인트의 인덱스를 섞어주는 함수
    private int[] ShuffleIndices(int length)
    {
        int[] indices = new int[length];
        for (int i = 0; i < length; i++)
        {
            indices[i] = i;
        }

        // Fisher-Yates 알고리즘으로 배열 섞기
        for (int i = length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        return indices;
    }
}
