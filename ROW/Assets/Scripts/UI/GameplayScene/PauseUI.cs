using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private InputReader inputReader = default;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainPanel;

    void Awake()
    {
        Debug.Assert(inputReader == null || pausePanel != null || mainPanel != null, "PauseUI: One or more panel references are missing.");

        pausePanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pausePanel.activeSelf)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        mainPanel.SetActive(false);
        inputReader.DisableAllInput();
        TimeManager.Instance.Pause();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(true);
        inputReader.EnableGameplayInput();
        TimeManager.Instance.Resume();
    }

}
