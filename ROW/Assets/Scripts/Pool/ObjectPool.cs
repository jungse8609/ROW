using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly GameObject _prefab;
    private readonly Queue<GameObject> _poolQueue;
    private readonly Transform _parent;

    /// <summary>
    /// ObjectPool Ŭ������ ������.
    /// </summary>
    /// <param name="prefab">Ǯ���� ������.</param>
    /// <param name="initialSize">�ʱ� Ǯ ũ��.</param>
    /// <param name="parent">Ǯ ��ü�� �θ� Transform.</param>
    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _poolQueue = new Queue<GameObject>();
        _parent = parent;

        // �ʱ� ��ü ����
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Ǯ���� ��ü�� �����ɴϴ�. Ǯ�� ��������� �� ��ü�� �����մϴ�.
    /// </summary>
    /// <returns>Ǯ���� GameObject.</returns>
    public GameObject GetObject()
    {
        if (_poolQueue.Count > 0)
        {
            GameObject obj = _poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return CreateNewObject();
        }
    }

    /// <summary>
    /// ��ü�� Ǯ�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="obj">��ȯ�� GameObject.</param>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _poolQueue.Enqueue(obj);
    }

    /// <summary>
    /// �� ��ü�� �����ϰ� Ǯ�� �߰��մϴ�.
    /// </summary>
    /// <returns>���� ������ GameObject.</returns>
    private GameObject CreateNewObject()
    {
        GameObject obj = Object.Instantiate(_prefab, _parent);
        obj.SetActive(false);
        _poolQueue.Enqueue(obj);
        return obj;
    }
}
