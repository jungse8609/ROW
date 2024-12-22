using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private float m_fTime;
    private int m_iMin;
    private float m_fSec;
    private TextMeshProUGUI m_TextUI;

    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_fTime = 0.0f;
        m_TextUI = this.gameObject.GetComponent<TextMeshProUGUI>();
        if(null == m_TextUI) {
            Debug.Log("TimerUI : m_TextUI is null ");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_fSec += Time.deltaTime;
        if(m_fSec >= 60f)
        {
            m_iMin += 1;
            m_fSec -= 60f;
        }
        m_fTime += Time.deltaTime;
        m_TextUI.text = string.Format("{0:D2}:{1:D2}", m_iMin, Mathf.FloorToInt(m_fSec));
    }
}
