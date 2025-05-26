using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get { init(); return s_instance; } }
    private static TimeManager s_instance;

    private int pauseCount = 0;
    
    private static void init()
    {
        if(s_instance == null)
        {
            GameObject gameobject = GameObject.Find("@TimeManager");
            if (null == gameobject)
            {
                gameobject = new GameObject { name = "@TimeManager" };
                s_instance = gameobject.AddComponent<TimeManager>();
            }
            else
            {
                s_instance = gameobject.GetComponent<TimeManager>();
            }
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        s_instance = null;
    }
    public string GetGamePlayTime()
    {
        float minute = Time.time / 60f;
        float second = Time.time % 60f;

        return string.Format("{0:D2}:{1:D2}", Mathf.FloorToInt(minute), Mathf.FloorToInt(second));
    }

    public void Pause()
    {
        ++pauseCount;
        UpdateTimeScale();
    }

    public void Resume()
    {
        --pauseCount;
        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        pauseCount = Mathf.Clamp(pauseCount, 0, pauseCount);
        Time.timeScale = pauseCount == 0 ? 1f : 0f;
    }

}

