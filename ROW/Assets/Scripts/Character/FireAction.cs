using UnityEngine;

public class FireAction : MonoBehaviour
{
    [SerializeField] public GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;
    [SerializeField] private ParticleSystem _muzzleFlash;

    private Player _player;
    private Animator _anim;
    private Gun _gun;

    private float _bulletTimer = 0.0f;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _gun = _gunObject.GetComponent<Gun>();
    }

    public void ReplaceGun(Gun newGun)
    {
        _gunObject = newGun.gameObject;
        _gun = newGun;
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
            _anim.Play("Rebound");
            
            if (!_gun.isReloading)
            { // 총 발사 시 이펙트
                _muzzleFlash.Play();
            }
            _gun.Fire();

            _bulletTimer = _playerStat.BulletCooltime;
        }
    }
}
