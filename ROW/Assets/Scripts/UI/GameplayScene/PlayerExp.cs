using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    private Player _player;
    private LevelManager _levelManager;
    private Slider _ExpBarSlider;
    private float _currentExp;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _levelManager = _player.GetComponent<LevelManager>();
        _ExpBarSlider = GetComponent<Slider>();
    }

    void Start()
    {
        _currentExp = 0;
        _ExpBarSlider.value = 0;
    }

    void Update()
    {
        if(_currentExp != _levelManager._expCount)
        {
            _currentExp = Mathf.Lerp(_currentExp, _levelManager._expCount, Time.deltaTime * 5);
            _ExpBarSlider.value = _currentExp / _levelManager.CalculateRequiredExpForNextLevel();
        }
    }
}
