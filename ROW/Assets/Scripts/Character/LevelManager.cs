using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private int _currentLevel = 0;

    [SerializeField] private int _expCount = 0;

    [SerializeField] private UnityEvent _levelUpEvent = new UnityEvent();  // LevelUp시 수행을 위해 추가됨

    public void GetExp()
    {
        _expCount += 1;

        // if level up 되는 exp, 레벨업
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
