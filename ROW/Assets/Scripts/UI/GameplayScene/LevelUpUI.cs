using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelupPanel;
    [SerializeField] private PlayerStatSO _playerStat;
    [SerializeField] private Button[] m_Buttons;


    private int[] m_iGeneratedStat;

    public bool m_isPause = false;

    void Start()
    {
        m_LevelupPanel.SetActive(false);
        m_isPause = false;
    }

    private void GenerateRandomLevelupOption()  
    {
        m_iGeneratedStat = GetRandomValues(PlayerStatSO.PLAYERSTATCOUNT, m_Buttons.Length);

        for (int i = 0; i < 3; i++)
        {
            MatchStatAndUI(i, m_iGeneratedStat[i]);
        }
    }




    private void MatchStatAndUI(int ButtonIndex, int StatIndex)
    {
        LevelupStatDescription desc = _playerStat.GetLevelupStatDescription(StatIndex);

        // 0 : button, 1: Icon
        TextMeshProUGUI[] texts = m_Buttons[ButtonIndex].GetComponentsInChildren<TextMeshProUGUI>(); 
        texts[0].text = desc.title;
        texts[1].text = desc.description;

        m_Buttons[ButtonIndex].GetComponentsInChildren<Image>()[1].sprite = desc.sprite; 
    }

    public void Levelup(int _Buttonindex)       //Button�� OnClick���� ����Ǵ� �Լ�
    {
        _playerStat.LevelupStat(m_iGeneratedStat[_Buttonindex]);
    } 

    public int[] GetRandomValues(int max, int count)
    {
        // ������ �� �ִ� ������ ����Ʈ ����
        List<int> availableValues = new List<int>();
        for (int i = 0; i <= max; i++)
        {
            availableValues.Add(i);
        }

        // ���� �� ����
        HashSet<int> selectedValues = new HashSet<int>();
        while (selectedValues.Count < count && availableValues.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableValues.Count);
            selectedValues.Add(availableValues[randomIndex]);
            availableValues.RemoveAt(randomIndex); // �ߺ� ������ ���� ������ �� ����
        }

        return new List<int>(selectedValues).ToArray(); // �迭�� ��ȯ�Ͽ� ��ȯ
    }

    public void Check_UIIsOn()      //Level UI�� �� ���¿��� ESC������ ��쿡 �����ϴ� �Լ�
    {
        if (m_isPause == true)
        {
            Time.timeScale = 0f;
            m_LevelupPanel.SetActive(true);
        }
        else
        {
            Resume_Levelup();
        }
    }

    public void Pause_Levelup()
    {
        m_isPause = true;
        m_LevelupPanel.SetActive(true);
        GenerateRandomLevelupOption();
        Time.timeScale = 0f;
    }

    public void Resume_Levelup()
    {
        m_isPause = false;
        Time.timeScale = 1f;
        m_LevelupPanel.SetActive(false);
    }
}
