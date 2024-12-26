using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected PlayerStatSO _playerStat = default;

    [Header("Bullet Pool Setting")]
    [SerializeField] protected ObjectPoolManagerSO _bulletPool = default;
    [SerializeField] private Transform _poolParent;
    public Transform PoolParent { get { return _poolParent; } }

    [Header("Gun Setting")]
    [SerializeField] protected Transform _firePoint;
    [SerializeField] private int _maxBulletCount = 12;
    [SerializeField] protected int _currentBulletCount = 0;

    [Header("Sound Setting")]
    protected AudioPlayer _audioPlayer;
    [SerializeField] protected AudioClip _fireAudio;
    [SerializeField] private AudioClip _reloadAudio;

    public float MaxBulletCount { get { return _maxBulletCount; } }
    public float CurrentBulletCount { get { return _currentBulletCount; } }
    
    public bool isReloading = false;

    public void InitPoolParent(Transform parent)
    {
        _poolParent = parent;
    }

    private void Awake()
    {
        _playerStat.InitVariables();

        _bulletPool.InitializePool(_poolParent);
        _currentBulletCount = 12;
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _audioPlayer = playerObject.GetComponent<AudioPlayer>();
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

        // Fire Sound
        _audioPlayer.PlayAudioClip(_fireAudio);

        if (_currentBulletCount <= 0)
        {
            Reload();
        }
    }

    public virtual void Reload()
    {
        if (isReloading == true)
            return;

        // Reload Sound
        _audioPlayer.PlayAudioClip(_reloadAudio);

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