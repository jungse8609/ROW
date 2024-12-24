using UnityEngine;

public class ReloadAction : MonoBehaviour
{
    [SerializeField] private GameObject _gunObject;
    [SerializeField] private PlayerStatSO _playerStat = default;
    [SerializeField] private AudioSource _reloadAudioSource;

    private Gun _gun;
    private Player _player;

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
            PlayReloadSound();
            _gun.Reload();
            _player.reloadInput = false;
        }
    }
    private void PlayReloadSound()
    {
        if (_reloadAudioSource != null && !_reloadAudioSource.isPlaying)
        {
            _reloadAudioSource.Play(); // AudioSource의 사운드 재생
        }
    }
}
