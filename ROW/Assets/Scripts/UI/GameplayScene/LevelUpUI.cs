using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat;
    [SerializeField] private Button[] _Buttons;

    private int[] m_iGeneratedStat;

    public static bool m_isPause = false;

    void Start()
    {
        gameObject.SetActive(false);
        m_isPause = false;
    }

    private void GenerateRandomLevelupOption()  
    {
        m_iGeneratedStat = GetRandomValues(0, 6, 3);  // 0~ 6 �ߺ����� 3�� ����

        for (int i = 0; i < 3; i++)
        {
            MatchStatAndUI(i, m_iGeneratedStat[i]);
        }
    }

    private void MatchStatAndUI(int ButtonIndex, int StatIndex)      // ������ ��ư�� ������ index�� �´� ���� ó��
    {

        LevelupStatDescription desc = _playerStat.GetLevelupStatDescription(StatIndex);

        Debug.Log(desc.sprite);
        _Buttons[ButtonIndex].GetComponentsInChildren<Image>()[1].sprite = desc.sprite; // 0 : button, 1: Icon

        TextMeshProUGUI[] texts = _Buttons[ButtonIndex].GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = desc.title;
        texts[1].text = desc.description;

    }

    public void Levelup(int _Buttonindex)       //Button�� OnClick���� ����Ǵ� �Լ�
    {
        _playerStat.LevelupStat(m_iGeneratedStat[_Buttonindex]);
        //Resume_Levelup();
    } 

    public int[] GetRandomValues(int min, int max, int count)
    {
        // ������ �� �ִ� ������ ����Ʈ ����
        List<int> availableValues = new List<int>();
        for (int i = min; i <= max; i++)
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
            gameObject.SetActive(true);
            //GenerateRandomLevelupOption();
        }
        else
        {
            Resume_Levelup();
        }
    }

    public void Pause_Levelup()
    {
        m_isPause = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        GenerateRandomLevelupOption();
    }

    public void Resume_Levelup()
    {
        m_isPause = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
