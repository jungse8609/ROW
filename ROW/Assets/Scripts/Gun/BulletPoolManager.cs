using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; // 총알 프리팹
    [SerializeField] private int _poolSize = 20; // 초기 풀 크기

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab); 
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // 풀에 남은 총알이 없으면 새로 생성
            GameObject newBullet = Instantiate(_bulletPrefab);
            newBullet.SetActive(false);
            return newBullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
