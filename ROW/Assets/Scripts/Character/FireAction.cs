using UnityEngine;

public class FireAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;

    private Gun _gun;
    private Player _player;
    private Animator _anim;

    private float _bulletTimer = 0.0f;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
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
            _anim.Play("Rebound");
            _gun.Fire();
            _bulletTimer = _playerStat.BulletCooltime;
        }
    }
}
