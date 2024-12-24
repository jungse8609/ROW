using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] 
    private PlayerStatSO _playerStat;

    [SerializeField]
    private Player _player;


    private Slider HpBarSlider;
    private float m_fCurrentHp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_fCurrentHp = _playerStat.CurrentHp;

        HpBarSlider = GetComponent<Slider>();
        HpBarSlider.value = _playerStat.CurrentHp / _playerStat.MaxHp;

    }

    // Update is called once per frame
    void Update()
    {
        // for test
        if(Input.GetKeyDown(KeyCode.I))
        {
            _playerStat.CurrentHp -= 1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _playerStat.CurrentHp += 1;
        }

        // 체력이 다 차있다면 보이지 않기
        //if (m_fCurrentHp == _playerStat.MaxHp)
        //{
        //    gameObject.SetActive(false);
        //}
        //else
        //{
        //    gameObject.SetActive(true);
        //}

        gameObject.transform.position = new Vector3(0, 60, 0) + Camera.main.WorldToScreenPoint(_player.transform.position);
        if(m_fCurrentHp != _playerStat.CurrentHp)
        {
            m_fCurrentHp = Mathf.Lerp(m_fCurrentHp, _playerStat.CurrentHp, Time.deltaTime * 5);
            HpBarSlider.value = m_fCurrentHp / _playerStat.MaxHp;
        }

    }
}
