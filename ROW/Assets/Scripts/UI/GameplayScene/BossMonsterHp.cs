using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHp : MonoBehaviour
{
    [SerializeField] private MonsterStatSO _MonsterSO;

    [SerializeField] private BossMonster _Monster;
    private Slider _hpBarSlider;
    private float _currentHp;

    private void Awake()
    {
        _hpBarSlider = GetComponent<Slider>();
    }

    void Start()
    {
        _currentHp = _Monster._currentHp;

        
        _hpBarSlider.value = _Monster._currentHp / _MonsterSO.MaxHealth;
    }

    void Update()
    {
        transform.position = new Vector3(0,100, 0) + Camera.main.WorldToScreenPoint(_Monster.transform.position);

        if(_currentHp != _Monster._currentHp)
        {
            _currentHp = Mathf.Lerp(_currentHp, _Monster._currentHp, Time.deltaTime * 5);
            _hpBarSlider.value = _currentHp / _MonsterSO.MaxHealth;
        }
    }
}
