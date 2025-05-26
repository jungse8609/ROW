using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    void Start()
    {
        textUI = gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Assert(textUI == null, "TimerUI: textUI references are missing.");
    }
    void Update()
    {
        textUI.text = TimeManager.Instance.GetGamePlayTime();
    }
}
