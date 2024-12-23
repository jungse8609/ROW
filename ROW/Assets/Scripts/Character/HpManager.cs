using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat = default;

    [Header("HP �ڵ� ȸ�� ����")]
    [SerializeField] private float _regenInterval = 2f; // �� �ʸ��� HP�� ȸ������

    [Header("�ǰ� �� ȸ�� ���� ����")]
    [SerializeField] private float _regenDelayAfterDamage = 3f; // �� �� ���� ȸ���� �ߴܵ���

    private float _regenTimer = 0f;       // �ڵ� ȸ�� �ֱ� üũ��
    private float _damageDelayTimer = 0f; // �ǰ� �� ȸ�� ���� Ÿ�̸�

    private void Start()
    {
        _regenTimer = _regenInterval;
    }

    private void Update()
    {
        // HP�� 0 ���϶�� ��� ó��
        if (_playerStat.CurrentHp <= 0)
        {
            Die();
            return;
        }

        // �ǰ� �� ȸ�� ���� �ð��� �����ִٸ� �켱 ���� Ÿ�̸� ����
        if (_damageDelayTimer > 0f)
        {
            _damageDelayTimer -= Time.deltaTime;
            return;
        }

        // ȸ�� ������ �����ٸ� regenTimer�� ���ҽ�Ű�� üũ
        _regenTimer -= Time.deltaTime;
        if (_regenTimer <= 0f)
        {
            // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ȸ�� ����
            _playerStat.CurrentHp += _playerStat.RegenAmount;
            _playerStat.CurrentHp = Mathf.Clamp(_playerStat.CurrentHp, 0, _playerStat.MaxHp);

            // ���� ȸ������ �ٽ� _regenInterval�� ����
            _regenTimer = _regenInterval;
        }
    }

    /// <summary>
    /// �������� �Ծ� ü���� ���ҽ�Ű��, �ǰ� �� ���� �ð� ȸ�� �ߴ�
    /// </summary>
    public void GetDamaged(float damage)
    {
        _playerStat.CurrentHp -= damage;

        if (_playerStat.CurrentHp <= 0)
        {
            _playerStat.CurrentHp = 0;
            Die();
            return;
        }

        // �ǰ� �������Ƿ�, ȸ�� ���� Ÿ�̸Ӹ� �缳��
        _damageDelayTimer = _regenDelayAfterDamage;

        // ȸ�� �ֱ� Ÿ�̸Ӹ� �ʱ�ȭ�ؼ�, ȸ�� ������ ���� ��
        // �ٽ� _regenInterval��ŭ ��ٷ��� ���� ȸ���� �����ϵ���.
        _regenTimer = _regenInterval;
    }

    private void Die()
    {
        Debug.Log("�÷��̾� ���!");
        // ��� ���� (������Ʈ ��Ȱ��ȭ, ���� ����, �� ��ȯ ��)�� ���⿡ �߰�
        // gameObject.SetActive(false);
        // or SceneManager.LoadScene("GameOverScene");
    }
}
