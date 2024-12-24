using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat;

    private Player _player;
    private Slider _hpBarSlider;
    private float _currentHp;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _hpBarSlider = GetComponent<Slider>();
    }

    void Start()
    {
        _currentHp = _playerStat.CurrentHp;

        
        _hpBarSlider.value = _playerStat.CurrentHp / _playerStat.MaxHp;
    }

    void Update()
    {
        transform.position = new Vector3(0, 60, 0) + Camera.main.WorldToScreenPoint(_player.transform.position);

        if(_currentHp != _playerStat.CurrentHp)
        {
            _currentHp = Mathf.Lerp(_currentHp, _playerStat.CurrentHp, Time.deltaTime * 5);
            _hpBarSlider.value = _currentHp / _playerStat.MaxHp;
        }
    }
}
