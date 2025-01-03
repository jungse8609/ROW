using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HpManager : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat = default;
    [SerializeField] private GameObject _gameoverPopup;

    [Header("HP 자동 회복 설정")]
    [SerializeField] private float _regenInterval = 2f; // 몇 초마다 HP를 회복할지

    [Header("피격 후 회복 지연 설정")]
    [SerializeField] private float _regenDelayAfterDamage = 3f; // 몇 초 동안 회복이 중단될지
    [SerializeField] private AudioClip _DieAudioClip; // 사망시 사운드
    [SerializeField] private AudioClip _hitSoundClip; // 피격시 사운드
    private AudioPlayer _playerAudio;

    private float _regenTimer = 0f;       // 자동 회복 주기 체크용
    private float _damageDelayTimer = 0f; // 피격 후 회복 지연 타이머
    private bool isDead = false; // 사망여부를 추적하는 변수

    private void Awake()
    {
        _playerAudio = GetComponent<AudioPlayer>();
    }
    private void OnEnable()
    {
        _playerStat.CurrentHp = _playerStat.MaxHp;
        isDead = false;  // 활성화될 때 사망 상태 초기화
    }

    private void Start()
    {
        _regenTimer = _regenInterval;
    }

    private void Update()
    {
        // HP가 0 이하라면 사망 처리
        if (_playerStat.CurrentHp <= 0)
        {
            Die();
            return;
        }

        // 피격 후 회복 지연 시간이 남아있다면 우선 지연 타이머 감소
        if (_damageDelayTimer > 0f)
        {
            _damageDelayTimer -= Time.deltaTime;
            return;
        }

        // 회복 지연이 끝났다면 regenTimer를 감소시키며 체크
        _regenTimer -= Time.deltaTime;
        if (_regenTimer <= 0f)
        {
            // 타이머가 0 이하가 되면 회복 수행
            _playerStat.CurrentHp += _playerStat.RegenAmount;
            _playerStat.CurrentHp = Mathf.Clamp(_playerStat.CurrentHp, 0, _playerStat.MaxHp);

            // 다음 회복까지 다시 _regenInterval로 설정
            _regenTimer = _regenInterval;
        }

    }

    /// <summary>
    /// 데미지를 입어 체력을 감소시키고, 피격 후 일정 시간 회복 중단
    /// </summary>
    public void GetDamaged(float damage)
    {
        _playerStat.CurrentHp -= damage;
        _playerAudio.PlayAudioClip(_hitSoundClip); // 피격시 사운드

        if (_playerStat.CurrentHp <= 0)
        {
            _playerStat.CurrentHp = 0;
            Die();
            return;
        }

        // 피격 당했으므로, 회복 지연 타이머를 재설정
        _damageDelayTimer = _regenDelayAfterDamage;

        // 회복 주기 타이머를 초기화해서, 회복 지연이 끝난 뒤
        // 다시 _regenInterval만큼 기다려야 다음 회복이 가능하도록.
        _regenTimer = _regenInterval;
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
        isDead = true;  // 사망 상태로 변경
        _playerAudio.PlayAudioClip(_DieAudioClip); // 사망시 사운드

        // 사망 로직 (오브젝트 비활성화, 게임 오버, 씬 전환 등)을 여기에 추가
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
