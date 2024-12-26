using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HpManager : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat = default;
    [SerializeField] private GameObject _gameoverPopup;

    [Header("HP �ڵ� ȸ�� ����")]
    [SerializeField] private float _regenInterval = 2f; // �� �ʸ��� HP�� ȸ������

    [Header("�ǰ� �� ȸ�� ���� ����")]
    [SerializeField] private float _regenDelayAfterDamage = 3f; // �� �� ���� ȸ���� �ߴܵ���
    [SerializeField] private AudioClip _DieAudioClip; // ����� ����
    [SerializeField] private AudioClip _hitSoundClip; // �ǰݽ� ����
    private AudioPlayer _playerAudio;

    private float _regenTimer = 0f;       // �ڵ� ȸ�� �ֱ� üũ��
    private float _damageDelayTimer = 0f; // �ǰ� �� ȸ�� ���� Ÿ�̸�
    private bool isDead = false; // ������θ� �����ϴ� ����

    private void Awake()
    {
        _playerAudio = GetComponent<AudioPlayer>();
    }
    private void OnEnable()
    {
        _playerStat.CurrentHp = _playerStat.MaxHp;
        isDead = false;  // Ȱ��ȭ�� �� ��� ���� �ʱ�ȭ
    }

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
        _playerAudio.PlayAudioClip(_hitSoundClip); // �ǰݽ� ����

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
        isDead = true;  // ��� ���·� ����
        _playerAudio.PlayAudioClip(_DieAudioClip); // ����� ����

        // ��� ���� (������Ʈ ��Ȱ��ȭ, ���� ����, �� ��ȯ ��)�� ���⿡ �߰�
        Time.timeScale = 0.0f;
        _gameoverPopup.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            GetDamaged(monster.AttackDamage());
        }
    }
}
