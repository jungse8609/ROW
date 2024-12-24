using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private readonly GameObject _prefab;
    private readonly Queue<GameObject> _poolQueue;
    private readonly Transform _parent;

    /// <summary>
    /// ObjectPool 클래스의 생성자.
    /// </summary>
    /// <param name="prefab">풀링할 프리팹.</param>
    /// <param name="initialSize">초기 풀 크기.</param>
    /// <param name="parent">풀 객체의 부모 Transform.</param>
    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _poolQueue = new Queue<GameObject>();
        _parent = parent;

        // 초기 객체 생성
        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// 풀에서 객체를 가져옵니다. 풀이 비어있으면 새 객체를 생성합니다.
    /// </summary>
    /// <returns>풀링된 GameObject.</returns>
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
    /// 객체를 풀에 반환합니다.
    /// </summary>
    /// <param name="obj">반환할 GameObject.</param>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _poolQueue.Enqueue(obj);
    }

    /// <summary>
    /// 새 객체를 생성하고 풀에 추가합니다.
    /// </summary>
    /// <returns>새로 생성된 GameObject.</returns>
    private GameObject CreateNewObject()
    {
        GameObject obj = Object.Instantiate(_prefab, _parent);
        obj.SetActive(false);
        _poolQueue.Enqueue(obj);
        return obj;
    }
}
