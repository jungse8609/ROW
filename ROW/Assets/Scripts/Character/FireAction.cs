using UnityEngine;

public class FireAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;

    private Player _player;
    private Gun _gun;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _gun = _gunObject.GetComponent<Gun>();
    }

    private void Update()
    {
        if (_player.fireInput)
        {
            _gun.Fire();
            _player.fireInput = false;
        }
    }
}
