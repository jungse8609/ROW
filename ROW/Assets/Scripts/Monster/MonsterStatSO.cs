using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatSO", menuName = "Monster/Monster Stat", order = 0)]
public class MonsterStatSO : ScriptableObject
{
    public float MoveSpeed = 2.0f;
    public float AttackDamage = 0.2f;
    public float AttackRange = 1.5f;
    public float MaxHealth = 10.0f;
}
