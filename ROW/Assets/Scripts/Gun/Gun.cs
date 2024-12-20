using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _firePoint; // 발사 위치
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _bulletDamage = 5f;

    private BulletPoolManager _bulletPool = default;

    private void Awake()
    {
        _bulletPool = GetComponent<BulletPoolManager>();
    }

    public virtual void Fire()
    {
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
    }
}
