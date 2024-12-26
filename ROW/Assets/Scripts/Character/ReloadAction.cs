using UnityEngine;
using UnityEngine.Events;

public class ReloadAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;

    private Gun _gun;
    private Player _player;

    [SerializeField] private UnityEvent _reloadEvent;

    private void Awake()
    {
        _player = GetComponent<Player>();
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
        if (_player.reloadInput)
        {
            _gun.Reload();
            _reloadEvent.Invoke();                  // Ui On
            _player.reloadInput = false;
        }
    }
}
