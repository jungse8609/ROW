using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private PlayerStatSO playerStat;
    [SerializeField] private Button[] buttons;

    private int[] generatedStats;

    void Awake()
    {
        levelUpPanel.SetActive(false);
    }

    private void GenerateRandomLevelupOption()  
    {
        generatedStats = GetRandomValues(PlayerStatSO.PLAYERSTATCOUNT, buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            MatchStatAndUI(i, generatedStats[i]);
        }
    }

    private void MatchStatAndUI(int ButtonIndex, int StatIndex)
    {
        LevelupStatDescription desc = playerStat.GetLevelupStatDescription(StatIndex);

        TextMeshProUGUI title = buttons[ButtonIndex].transform.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        title.text = desc.title;

        TextMeshProUGUI description = buttons[ButtonIndex].transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        description.text = desc.description;

        Image image = buttons[ButtonIndex].transform.Find("Icon").GetComponent<Image>();
        image.sprite = desc.sprite; 
    }

    public void Levelup(int buttonIndex) //Button의 OnClick으로 실행되는 함수
    {
        playerStat.LevelupStat(generatedStats[buttonIndex]);
    } 

    public int[] GetRandomValues(int max, int count)
    {
        // 선택할 수 있는 숫자의 리스트 생성
        List<int> availableValues = new List<int>();
        for (int i = 0; i <= max; i++)
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

    public void Pause()
    {
        GenerateRandomLevelupOption();
        levelUpPanel.SetActive(true);
        TimeManager.Instance.Pause();
    }

    public void Resume()
    {
        levelUpPanel.SetActive(false);
        TimeManager.Instance.Resume();
    }
}
