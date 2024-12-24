using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat = default;

    [Header("Bullet Pool Setting")]
    [SerializeField] private ObjectPoolManagerSO _bulletPool = default;
    [SerializeField] private Transform _poolParent;

    [Header("Gun Setting")]
    [SerializeField] private Transform _firePoint; // 발사 위치
    [SerializeField] private int _maxBulletCount = 12;
    [SerializeField] private int _currentBulletCount = 0;

    public float MaxBulletCount { get { return _maxBulletCount; } }
    public float CurrentBulletCount { get { return _currentBulletCount; } }

    
    private bool isReloading = false;

    private void Awake()
    {
        _bulletPool.InitializePool(_poolParent);
        _currentBulletCount = 12;
    }

    public virtual void Fire()
    {
        if (isReloading) return;

        if (_currentBulletCount <= 0)
        {
            Reload();
            return;
        }

        // Create Bullet Prefab
        GameObject bullet = _bulletPool.GetObject();
        bullet.transform.position = _firePoint.position;

        // firePoint의 현재 회전 값을 Euler Angles로 가져옴
        Vector3 fireRotation = _firePoint.eulerAngles;
        fireRotation.x = fireRotation.z = 0f;
        bullet.transform.rotation = Quaternion.Euler(fireRotation);

        TrailRenderer trail = bullet.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }

        if (bullet.TryGetComponent<Bullet>(out Bullet bulletComponent))
        {
            bulletComponent.InitBullet(_playerStat.BulletSpeed, _playerStat.BulletSpeed);
        }
        
        _currentBulletCount -= 1;

        if (_currentBulletCount <= 0)
        {
            Reload();
        }
    }

    public virtual void Reload()
    {
        if (isReloading == true)
            return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(_playerStat.ReloadCooltime);

        _currentBulletCount = _maxBulletCount;

        isReloading = false;
    }
}
