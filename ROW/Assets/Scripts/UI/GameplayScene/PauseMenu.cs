using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    [SerializeField] private GameObject m_PausePanel;
    [SerializeField] private GameObject m_MainPanel;
    [SerializeField] private UnityEvent m_LevelupEvent;

    [SerializeField] private bool m_isPause = false;

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
        _inputReader.EnableGameplayInput();
        Time.timeScale = 1f;
        m_LevelupEvent.Invoke();
    }
    public void Pause()
    {
        m_isPause = true;
        m_PausePanel.SetActive(true);
        m_MainPanel.SetActive(false);
        _inputReader.DisableAllInput();
        Time.timeScale = 0f;
    }
}
