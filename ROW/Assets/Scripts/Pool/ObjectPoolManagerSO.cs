using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolManager", menuName = "Pool/Object Pool", order = 0)]
public class ObjectPoolManagerSO : ScriptableObject
{
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected int _initialSize;
    
    protected Transform _parent = default;
    protected Queue<GameObject> _objectPool = new Queue<GameObject>();

    public virtual void InitializePool(Transform parent)
    {
        _parent = parent;

        for (int i = 0; i < _initialSize; i++)
        {
            GameObject obj = CreateNewObject();
            _objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (_objectPool.Count > 0)
        {
            GameObject obj = _objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = CreateNewObject();
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(_prefab);
        newObj.transform.parent = _parent;
        newObj.SetActive(false);
        return newObj;
    }
}
