using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Setting")]
    [SerializeField] private Transform _firePoint; // 발사 위치
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _bulletDamage = 5f;
    [SerializeField] private float _bulletCooltime = 1.0f;
    public float BulletCooltime { get { return _bulletCooltime; } }
    [SerializeField] private float _reloadCooltime = 2.0f;
    public float ReloadCooltime { get { return _reloadCooltime; } }

    [Header("Bullet Count Setting")]
    [SerializeField] private int _maxBulletCount = 12;
    public float MaxBulletCount { get { return _maxBulletCount; } }
    [SerializeField] private int _currentBulletCount = 0;
    public float CurrentBulletCount { get { return _currentBulletCount; } }

    private FireAction _fireActionScript = default;
    private BulletPoolManager _bulletPool = default;
    private bool isReloading = false;

    private void Awake()
    {
        _bulletPool = GetComponent<BulletPoolManager>();
        _fireActionScript = GetComponent<FireAction>();

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
        GameObject bullet = _bulletPool.GetBullet();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;

        TrailRenderer trail = bullet.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }

        if (bullet.TryGetComponent<Bullet>(out Bullet bulletComponent))
        {
            bulletComponent.InitBullet(_bulletPool, _bulletSpeed, _bulletDamage);
        }
        
        _currentBulletCount -= 1;

        if (_currentBulletCount <= 0)
        {
            Debug.Log("Spent all of the bullets => Reload");
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

        if (_fireActionScript != null)
        {
            _fireActionScript.enabled = false;
        }

        Debug.Log($"Before Reload : current bullet count = {_currentBulletCount}");

        yield return new WaitForSeconds(_reloadCooltime);

        if (_fireActionScript != null)
        {
            _fireActionScript.enabled = true;
        }

        _currentBulletCount = _maxBulletCount;
        Debug.Log($"After Reload : current bullet count = {_currentBulletCount}");

        isReloading = false;
    }
}
