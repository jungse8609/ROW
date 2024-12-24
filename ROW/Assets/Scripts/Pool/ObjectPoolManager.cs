using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �پ��� �������� ������Ʈ Ǯ�� �����ϴ� �̱��� �Ŵ��� Ŭ����.
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static ObjectPoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string PoolName;      // Ǯ�� �̸�
        public GameObject Prefab;    // Ǯ���� ������
        public int InitialSize;      // �ʱ� Ǯ ũ��
    }

    [Header("Pools Configuration")]
    [SerializeField] private List<Pool> _pools; // Ǯ ���� ����Ʈ

    private Dictionary<string, ObjectPool> _poolDictionary;

    private void Awake()
    {
        // �̱��� ���� ����
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
    /// ��� Ǯ�� �ʱ�ȭ�մϴ�.
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
    /// ������ Ǯ���� ��ü�� �����ɴϴ�.
    /// </summary>
    /// <param name="poolName">Ǯ�� �̸�.</param>
    /// <returns>Ǯ���� GameObject.</returns>
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
    /// ������ Ǯ�� ��ü�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="poolName">Ǯ�� �̸�.</param>
    /// <param name="obj">��ȯ�� GameObject.</param>
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
