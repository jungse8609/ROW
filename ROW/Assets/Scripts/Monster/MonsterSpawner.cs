using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Pool Setting")]
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private int _monsterPoolSize = 20; // 초기 풀 크기
    [SerializeField] private int _bossPoolSize = 3; // 초기 풀 크기

    [Header("Spawn Setting")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnInterval = 5.0f;
    [SerializeField] private int _mspawncount = 2; // 한 웨이브당 소환할 몬스터 수 (x값)

    // Spawn variables
    private float _spawnTimer;
    private int _waveCount = 0;
    private int _BOSS_WAVE = 5;

    // Pool variables
    private Queue<GameObject> _monsterPool = new Queue<GameObject>();
    private Queue<GameObject> _bossPool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _monsterPoolSize; i++)
        {
            GameObject bullet = Instantiate(_monsterPrefab);
            bullet.SetActive(false);
            _monsterPool.Enqueue(bullet);
        }

        for (int i = 0; i < _bossPoolSize; i++)
        {
            GameObject bullet = Instantiate(_bossPrefab);
            bullet.SetActive(false);
            _bossPool.Enqueue(bullet);
        }
    }

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
            int spawnCount = Mathf.Min(2, _spawnPoints.Length);
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(_monsterPrefab, _spawnPoints[shuffledIndices[i]].position, Quaternion.identity);
            }
        }

        if (_waveCount % _BOSS_WAVE == 0) // 5번째 웨이브마다 보스 소환
        {
            Instantiate(_bossPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
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

    public GameObject GetMonster()
    {
        if (_monsterPool.Count > 0)
        {
            GameObject monster = _monsterPool.Dequeue();
            monster.SetActive(true);
            return monster;
        }
        else
        {
            // 풀에 남은 총알이 없으면 새로 생성
            GameObject newMonster = Instantiate(_monsterPrefab);
            newMonster.SetActive(false);
            return newMonster;
        }
    }

    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        _monsterPool.Enqueue(monster);
    }

    public GameObject GetBoss()
    {
        if (_monsterPool.Count > 0)
        {
            GameObject boss = _bossPool.Dequeue();
            boss.SetActive(true);
            return boss;
        }
        else
        {
            // 풀에 남은 총알이 없으면 새로 생성
            GameObject newBoss = Instantiate(_bossPrefab);
            newBoss.SetActive(false);
            return newBoss;
        }
    }

    public void ReturnBoss(GameObject boss)
    {
        boss.SetActive(false);
        _bossPool.Enqueue(boss);
    }
}
