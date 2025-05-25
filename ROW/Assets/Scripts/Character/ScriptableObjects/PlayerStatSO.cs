using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Player/Plawer Stat", order = 0)]
public class PlayerStatSO : ScriptableObject
{
    public float MaxHp = 10.0f;
    public float RegenAmount = 0.2f;
    public float MoveSpeed = 2.0f;
    public float BulletSpeed = 4.0f;
    public float BulletDamage = 10.0f;
    public float BulletCooltime = 0.4f;
    public float ReloadCooltime = 3.0f;
    
    public float CurrentHp = 0.0f;

    public int MaxHpLevel = 0;
    public int RegenAmountLevel = 0;
    public int MoveSpeedLevel = 0;
    public int BulletSpeedLevel = 0;
    public int BulletDamageLevel = 0;
    public int BulletCooltimeLevel = 0;
    public int ReloadCooltimeLevel = 0;


    public static int PLAYERSTATCOUNT = 6;

    public void InitVariables()
    {
        MaxHp = 10.0f;
        CurrentHp = MaxHp;
        RegenAmount = 0.2f;
        MoveSpeed = 4.0f;
        BulletSpeed = 20.0f;
        BulletDamage = 10.0f;
        BulletCooltime = 1.0f;
        ReloadCooltime = 0.8f;

        MaxHpLevel = 0;
        RegenAmountLevel = 0;
        MoveSpeedLevel = 0;
        BulletSpeedLevel = 0;
        BulletDamageLevel = 0;
        BulletCooltimeLevel = 0;
        ReloadCooltimeLevel = 0;
    }

    public LevelupStatDescription GetLevelupStatDescription(int statIndex)
    {
        LevelupStatDescription desc = new LevelupStatDescription();

        switch (statIndex)
        {
            case 0:
                desc.sprite = Resources.Load<Sprite>("Image/MaxHp");
                desc.title = "최대 체력";
                desc.description = $"{MaxHp} -> {MaxHp + 2 * (MaxHpLevel + 1):F1}";
                break;
            case 1:
                desc.sprite = Resources.Load< Sprite>("Image/RegenAmount");
                desc.title = "체력 재생력";
                desc.description = $"{RegenAmount} -> {RegenAmount + 0.2 * (RegenAmountLevel + 1):F1}";
                break;
            case 2:
                desc.sprite = Resources.Load< Sprite>("Image/MoveSpeed");
                desc.title = "이동 속도";
                desc.description = $"{MoveSpeed} -> {MoveSpeed + .2f * (MoveSpeedLevel + 1):F1}";
                break;
            case 3:
                desc.sprite = Resources.Load<Sprite>("Image/BulletSpeed");
                desc.title = "총알 속도";
                desc.description = $"{BulletSpeed} -> {BulletSpeed + .2f * (BulletSpeedLevel + 1):F1}";
                break;
            case 4:
                desc.sprite = Resources.Load<Sprite>("Image/BulletDamage");
                desc.title = "총 데미지";
                desc.description = $"{BulletDamage} -> {BulletDamage + .2f * (BulletDamageLevel + 1):F1}";
                break;
            case 5:
                desc.sprite = Resources.Load<Sprite>("Image/BulletCooltime");
                desc.title = "발사 속도";
                desc.description = $"{BulletCooltime} -> {BulletCooltime - .2f * (BulletCooltimeLevel + 1):F1}";
                break;
            case 6:
                desc.sprite = Resources.Load<Sprite>("Image/ReloadCooltime");
                desc.title = "ReloadCooltime";
                desc.description = $"{ReloadCooltime} -> {ReloadCooltime - .2f * (ReloadCooltimeLevel + 1):F1}";
                break;
            default:
                break;
        }

        return desc;
    }

    // 실제 수치값 변경 구현 부탁드립니다
    public void LevelupStat(int statIndex)
    {
        switch (statIndex)
        {
        case 0:
            MaxHpLevel += 1;
            MaxHp += 2 * MaxHpLevel;
            CurrentHp += 2 * MaxHpLevel;
            break;
        case 1:
            RegenAmountLevel += 1;
            RegenAmount += 0.5f * RegenAmountLevel;
            break;
        case 2:
            MoveSpeedLevel += 1;
            MoveSpeed += 2 * MoveSpeedLevel;
            break;
        case 3:
            BulletSpeedLevel += 1;
            BulletSpeed += 2 * BulletSpeedLevel;
            break;
        case 4:
            BulletDamageLevel += 1;
            BulletDamage += 2 * BulletDamageLevel;
            break;
        case 5:
            BulletCooltimeLevel += 1;
            BulletCooltime -= 0.2f * BulletCooltimeLevel;
            BulletCooltime = Mathf.Clamp(BulletCooltime, 0.05f, 2.0f);
            break;
        case 6:
            ReloadCooltimeLevel += 1;
            ReloadCooltime -= 0.2f * ReloadCooltimeLevel;
            ReloadCooltime = Mathf.Clamp(ReloadCooltime, 0.05f, 2.0f);
            break;
        default:
            break;
        }
    }
}

public struct LevelupStatDescription
{
    public Sprite sprite;
    public string title;
    public string description;

    public static int TitleIndex = 0;
    public static int DescriptionIndex = 0;
};
