using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Player/Plawer Stat", order = 0)]
public class PlayerStatSO : ScriptableObject
{

    public float MaxHp = 10.0f;
    public float RegenAmount = 0.2f;
    public float MoveSpeed = 5.0f;

    public float BulletSpeed = 4.0f;
    public float BulletDamage = 10.0f;
    public float BulletCooltime = 1.0f;
    public float ReloadCooltime = 2.0f;

    public float CurrentHp = 0.0f;

    private void Awake()
    {
        CurrentHp = MaxHp;
    }

    // 각 케이스에 [현재 수치] -> [레벨 업이후의 수치] 형식으로 string 만들어서 return 해주시면 됩니다
    // 인덱스는 변수 순서와 동일
    // ex) 10 -> 13     (MaxHP)
    public LevelupStatDescription GetLevelupStatDescription(int statIndex)
    {
        LevelupStatDescription desc = new LevelupStatDescription();

        switch (statIndex)
        {
            case 0:
                desc.sprite = Resources.Load<Sprite>("Image/MaxHp");
                desc.title = "최대 체력";
                desc.description = $"{MaxHp} -> ";
                break;
            case 1:
                desc.sprite = Resources.Load< Sprite>("Image/RegenAmount");
                desc.title = "체력 재생력";
                desc.description = "";
                break;
            case 2:
                desc.sprite = Resources.Load< Sprite>("Image/MoveSpeed");
                desc.title = "이동 속도";
                desc.description = "";
                break;
            case 3:
                desc.sprite = Resources.Load<Sprite>("Image/BulletSpeed");
                desc.title = "총알 속도";
                desc.description = "";
                break;
            case 4:
                desc.sprite = Resources.Load<Sprite>("Image/BulletDamage");
                desc.title = "총 데미지";
                desc.description = "";
                break;
            case 5:
                desc.sprite = Resources.Load<Sprite>("Image/BulletCooltime");
                desc.title = "발사 속도";
                desc.description = "";
                break;
            case 6:
                desc.sprite = Resources.Load<Sprite>("Image/ReloadCooltime");
                desc.title = "ReloadCooltime";
                desc.description = "";
                break;
            default:
                break;
        }

        return desc;
    }

    // 실제 수치값 변경 구현 부탁드립니다
    public void LevelupStat(int statIndex)
    {
    }
}

public struct LevelupStatDescription
{
    public Sprite sprite;
    public string title;
    public string description;
};
