using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 다양한 프리팹의 오브젝트 풀을 관리하는 싱글톤 매니저 클래스.
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ObjectPoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string PoolName;      // 풀의 이름
        public GameObject Prefab;    // 풀링할 프리팹
        public int InitialSize;      // 초기 풀 크기
    }

    [Header("Pools Configuration")]
    [SerializeField] private List<Pool> _pools; // 풀 설정 리스트

    private Dictionary<string, ObjectPool> _poolDictionary;

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 모든 풀을 초기화합니다.
    /// </summary>
    private void InitializePools()
    {
        _poolDictionary = new Dictionary<string, ObjectPool>();

        foreach (var pool in _pools)
        {
            ObjectPool objectPool = new ObjectPool(pool.Prefab, pool.InitialSize, this.transform);
            _poolDictionary.Add(pool.PoolName, objectPool);
        }
    }

    /// <summary>
    /// 지정된 풀에서 객체를 가져옵니다.
    /// </summary>
    /// <param name="poolName">풀의 이름.</param>
    /// <returns>풀링된 GameObject.</returns>
    public GameObject GetObject(string poolName)
    {
        if (_poolDictionary.ContainsKey(poolName))
        {
            return _poolDictionary[poolName].GetObject();
        }
        else
        {
            Debug.LogWarning($"Pool with name {poolName} does not exist.");
            return null;
        }
    }

    /// <summary>
    /// 지정된 풀에 객체를 반환합니다.
    /// </summary>
    /// <param name="poolName">풀의 이름.</param>
    /// <param name="obj">반환할 GameObject.</param>
    public void ReturnObject(string poolName, GameObject obj)
    {
        if (_poolDictionary.ContainsKey(poolName))
        {
            _poolDictionary[poolName].ReturnObject(obj);
        }
        else
        {
            Debug.LogWarning($"Pool with name {poolName} does not exist.");
        }
    }
}
