using UnityEngine;

public class ReloadAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;

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
        if (_player.reloadInput)
        {
            _gun.Reload();
            _player.reloadInput = false;
        }
    }
}
