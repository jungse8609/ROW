using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Player/Plawer Stat", order = 0)]
public class PlayerStatSO : ScriptableObject
{
    public float MoveSpeed = 5.0f;

    public float BulletSpeed = 4.0f;
    public float BulletDamage = 10.0f;
    public float BulletCooltime = 1.0f;
    public float ReloadCooltime = 2.0f;
}
