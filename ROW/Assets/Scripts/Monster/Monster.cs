using UnityEngine;
using UnityEngine.AI; // NavMeshAgent 사용 시 필요

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;

    protected MonsterPoolManager _monsterPool = default;

    private Transform playerTransform;
    private NavMeshAgent navAgent; // NavMeshAgent 컴포넌트

    private string _poolName;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogError("Monster has no nav mesh!");
        }
    }

    /// <summary>
    /// 몬스터를 초기화합니다.
    /// </summary>
    public void InitMonster()
    {
        // 풀 이름을 태그나 다른 방법으로 설정할 수 있습니다.
        if (gameObject.CompareTag("Boss"))
        {
            _poolName = "BossPool";
        }
        else
        {
            _poolName = "MonsterPool";
        }

        // 추가 초기화 로직이 필요하다면 여기에 작성
    }

    private void OnEnable()
    {
        _monsterStat.CurrentHealth = _monsterStat.MaxHealth;
    }

    private void Start()
    {
        // Player 태그를 가진 오브젝트 찾기
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        // NavMeshAgent 기본 세팅
        if (navAgent != null)
        {
            // 이동 속도, 멈출 거리 설정
            navAgent.speed = _monsterStat.MoveSpeed;
            navAgent.stoppingDistance = _monsterStat.AttackRange;
        }
    }

    protected virtual void Update()
    {
        if (playerTransform == null || navAgent == null) return;

        navAgent.SetDestination(playerTransform.position);
    }

    public virtual void TakeDamage(float damage)
    {
        _monsterStat.CurrentHealth -= damage;
        if (_monsterStat.CurrentHealth <= 0)
        {
            OnDie();
        }
    }

    public float AttackDamage()
    {
        return _monsterStat.AttackDamage;
    }

    private void OnDie()
    {
        // 몬스터를 비활성화하고 풀에 반환
        ObjectPoolManager.Instance.ReturnObject(_poolName, gameObject);
    }
}
