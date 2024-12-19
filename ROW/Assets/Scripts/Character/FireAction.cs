using UnityEngine;

public class FireAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;


    private const float COOLTIME = 0.2f;
    private float _timer = 0.0f;
    private Gun _gun;
    private Player _player;


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
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (_player.fireInput && _timer <= 0.0f)
        {
            _gun.Fire();
            _timer = COOLTIME;
        }
    }
}
