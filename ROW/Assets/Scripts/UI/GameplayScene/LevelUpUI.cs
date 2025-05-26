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

    public void Levelup(int buttonIndex) //Button�� OnClick���� ����Ǵ� �Լ�
    {
        playerStat.LevelupStat(generatedStats[buttonIndex]);
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
