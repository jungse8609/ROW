using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private int _currentLevel = 0;

    [SerializeField] private int _expCount = 0;

    [SerializeField] private UnityEvent _levelUpEvent = new UnityEvent();  // LevelUp�� ������ ���� �߰���

    public void GetExp()
    {
        _expCount += 1;

        // if level up �Ǵ� exp, ������
        if (HasEnoughExpForLevelUp())
        {
            LevelUp();
            _expCount = 0;
        }
    }

    private void LevelUp()
    {
        _levelUpEvent.Invoke();
        _currentLevel += 1;
    }

    private bool HasEnoughExpForLevelUp()
    {
        if (_expCount >= CalculateRequiredExpForNextLevel()) 
            return true;
        return false;
    }

    private int CalculateRequiredExpForNextLevel()
    {
        return (_currentLevel + 1) * 5;
    }
}
