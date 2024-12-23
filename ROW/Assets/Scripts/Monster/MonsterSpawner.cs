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
        }
    }

    private void SpawnMonsterWave()
    {
        _waveCount += 1;

        if (_spawnPoints.Length >= 2)
        {
            int spawnIndex1 = Random.Range(0, _spawnPoints.Length);
            int spawnIndex2;
            
            // 같은 인덱스가 선택되지 않도록 보장
            do
            {
                spawnIndex2 = Random.Range(0, _spawnPoints.Length);
            } while (spawnIndex1 == spawnIndex2);

            // 두 위치에서 몬스터를 소환
            Instantiate(_monsterPrefab, _spawnPoints[spawnIndex1].position, Quaternion.identity);
            Instantiate(_monsterPrefab, _spawnPoints[spawnIndex2].position, Quaternion.identity);
        }

        if (_waveCount % _BOSS_WAVE == 0) // 5번째 웨이브마다 보스 소환
        {
            Instantiate(_bossPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        }
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
