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

    private void MatchStatAndUI(int _buttonIndex, int _statIndex)
    {
        LevelupStatDescription desc = playerStat.GetLevelupStatDescription(_statIndex);

        TextMeshProUGUI title = buttons[_buttonIndex].transform.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        title.text = desc.title;

        TextMeshProUGUI description = buttons[_buttonIndex].transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        description.text = desc.description;

        Image image = buttons[_buttonIndex].transform.Find("Icon").GetComponent<Image>();
        image.sprite = desc.sprite; 
    }

    public void Levelup(int _buttonIndex) //Button�� OnClick���� ����Ǵ� �Լ�
    {
        playerStat.LevelupStat(generatedStats[_buttonIndex]);
    } 

    public int[] GetRandomValues(int _max, int _count)
    {
        // ������ �� �ִ� ������ ����Ʈ ����
        List<int> availableValues = new List<int>();
        for (int i = 0; i <= _max; i++)
        {
            availableValues.Add(i);
        }

        // ���� �� ����
        HashSet<int> selectedValues = new HashSet<int>();
        while (selectedValues.Count < _count && availableValues.Count > 0)
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
