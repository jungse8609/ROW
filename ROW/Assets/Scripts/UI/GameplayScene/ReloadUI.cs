using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatSO _playerStatSO;
    private Player _player;
    private Slider _ReloadSlider;

    private float m_fTotalReloadTime = 1f;
    private float m_fCurrentReloadTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _ReloadSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(20, 30, 0) + Camera.main.WorldToScreenPoint(_player.transform.position);

        m_fCurrentReloadTime += Time.deltaTime;
        _ReloadSlider.value = m_fCurrentReloadTime / m_fTotalReloadTime;

        if(m_fCurrentReloadTime >= m_fTotalReloadTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartUI()
    {
        m_fTotalReloadTime = _playerStatSO.ReloadCooltime;
        m_fCurrentReloadTime = 0f;
        gameObject.SetActive(true);
    }
}
