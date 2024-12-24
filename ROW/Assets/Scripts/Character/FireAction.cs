using UnityEngine;

public class FireAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;
    [SerializeField] private ParticleSystem _muzzleFlash;

    private Gun _gun;
    private Player _player;

    private float _bulletTimer = 0.0f;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _gun = _gunObject.GetComponent<Gun>();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnUpdate()
    {
        if (_bulletTimer > 0)
        {
            _bulletTimer -= Time.deltaTime;
        }

        if (_player.fireInput && _bulletTimer <= 0.0f)
        {
            _gun.Fire();

            if (!_gun.isReloading){ // 총 발사 시 이펙트
                _muzzleFlash.Play();
            }

            _bulletTimer = _playerStat.BulletCooltime;
        }
    }
}
