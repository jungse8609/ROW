using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject m_PausePanel;
    public GameObject m_MainPanel;
    public static bool m_isPause = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(null == m_PausePanel || null == m_MainPanel)
        {
            Debug.Log("PauseMenu : Panel is null");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        m_PausePanel.SetActive(false);
        m_isPause = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        m_isPause = false;
        m_PausePanel.SetActive(false);
        m_MainPanel.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        m_isPause = true;
        m_PausePanel.SetActive(true);
        m_MainPanel.SetActive(false);
        Time.timeScale = 0f;
    }
}