using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Pool Names")]
    [SerializeField] private string _monsterPoolName = "MonsterPool"; // ObjectPoolManager에 설정한 풀 이름
    [SerializeField] private string _bossPoolName = "BossPool";       // ObjectPoolManager에 설정한 풀 이름

    [Header("Spawn Setting")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 5.0f;
    [SerializeField] private int _mspawncount = 2; // 한 웨이브당 소환할 몬스터 수 (x값)

    // Spawn variables
    private float _spawnTimer;
    private int _waveCount = 0;
    private int _BOSS_WAVE = 5;

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            SpawnMonsterWave();
            _spawnTimer = _spawnInterval;

            if (_waveCount % 3 == 0)
            {
                _mspawncount++;
            }
        }
    }

    private void SpawnMonsterWave()
    {
        _waveCount += 1;

        if (_spawnPoints.Length > 0)
        {
            // 스폰 포인트 중복을 방지하기 위해 인덱스를 섞음
            int[] shuffledIndices = ShuffleIndices(_spawnPoints.Length);

            // 최대 x마리의 몬스터를 소환 (스폰포인트가 부족하면 스폰포인트 개수만큼만 소환)
            int spawnCount = Mathf.Min(_mspawncount, _spawnPoints.Length);
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject monster = ObjectPoolManager.Instance.GetObject(_monsterPoolName);
                if (monster != null)
                {
                    monster.transform.position = _spawnPoints[shuffledIndices[i]].position;
                    monster.transform.rotation = _spawnPoints[shuffledIndices[i]].rotation;
                    monster.SetActive(true);

                    // 필요한 경우 추가 초기화
                    var monsterScript = monster.GetComponent<Monster>();
                    monsterScript?.InitMonster();
                }
            }
        }

        if (_waveCount % _BOSS_WAVE == 0) // 5번째 웨이브마다 보스 소환
        {
            GameObject boss = ObjectPoolManager.Instance.GetObject(_bossPoolName);
            if (boss != null && _spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Length);
                boss.transform.position = _spawnPoints[randomIndex].position;
                boss.transform.rotation = _spawnPoints[randomIndex].rotation;
                boss.SetActive(true);

                // 필요한 경우 추가 초기화
                var bossScript = boss.GetComponent<Monster>();
                bossScript?.InitMonster();
            }
        }
    }

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
