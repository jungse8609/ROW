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

    // �� ���̽��� [���� ��ġ] -> [���� �������� ��ġ] �������� string ���� return ���ֽø� �˴ϴ�
    // �ε����� ���� ������ ����
    // ex) 10 -> 13     (MaxHP)
    public LevelupStatDescription GetLevelupStatDescription(int statIndex)
    {
        LevelupStatDescription desc = new LevelupStatDescription();

        switch (statIndex)
        {
            case 0:
                desc.sprite = Resources.Load<Sprite>("Image/MaxHp");
                desc.title = "�ִ� ü��";
                desc.description = $"{MaxHp} -> ";
                break;
            case 1:
                desc.sprite = Resources.Load< Sprite>("Image/RegenAmount");
                desc.title = "ü�� �����";
                desc.description = "";
                break;
            case 2:
                desc.sprite = Resources.Load< Sprite>("Image/MoveSpeed");
                desc.title = "�̵� �ӵ�";
                desc.description = "";
                break;
            case 3:
                desc.sprite = Resources.Load<Sprite>("Image/BulletSpeed");
                desc.title = "�Ѿ� �ӵ�";
                desc.description = "";
                break;
            case 4:
                desc.sprite = Resources.Load<Sprite>("Image/BulletDamage");
                desc.title = "�� ������";
                desc.description = "";
                break;
            case 5:
                desc.sprite = Resources.Load<Sprite>("Image/BulletCooltime");
                desc.title = "�߻� �ӵ�";
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

    // ���� ��ġ�� ���� ���� ��Ź�帳�ϴ�
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
