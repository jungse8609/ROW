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
        m_iGeneratedStat = GetRandomValues(0, 6, 3);  // 0~ 6 중복없이 3개 선택

        for (int i = 0; i < 3; i++)
        {
            MatchStatAndUI(i, m_iGeneratedStat[i]);
        }
    }

    private void MatchStatAndUI(int ButtonIndex, int StatIndex)      // 선택한 버튼에 선택한 index에 맞는 설명 처리
    {

        LevelupStatDescription desc = _playerStat.GetLevelupStatDescription(StatIndex);

        Debug.Log(desc.sprite);
        _Buttons[ButtonIndex].GetComponentsInChildren<Image>()[1].sprite = desc.sprite; // 0 : button, 1: Icon

        TextMeshProUGUI[] texts = _Buttons[ButtonIndex].GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = desc.title;
        texts[1].text = desc.description;

    }

    public void Levelup(int _Buttonindex)       //Button의 OnClick으로 실행되는 함수
    {
        _playerStat.LevelupStat(m_iGeneratedStat[_Buttonindex]);
        //Resume_Levelup();
    } 

    public int[] GetRandomValues(int min, int max, int count)
    {
        // 선택할 수 있는 숫자의 리스트 생성
        List<int> availableValues = new List<int>();
        for (int i = min; i <= max; i++)
        {
            availableValues.Add(i);
        }

        // 랜덤 값 선택
        HashSet<int> selectedValues = new HashSet<int>();
        while (selectedValues.Count < count && availableValues.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableValues.Count);
            selectedValues.Add(availableValues[randomIndex]);
            availableValues.RemoveAt(randomIndex); // 중복 방지를 위해 선택한 값 제거
        }

        return new List<int>(selectedValues).ToArray(); // 배열로 변환하여 반환
    }

    public void Check_UIIsOn()      //Level UI가 뜬 상태에서 ESC눌렀을 경우에 실행하는 함수
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
