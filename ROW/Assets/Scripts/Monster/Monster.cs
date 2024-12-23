using UnityEngine;
using UnityEngine.AI; // NavMeshAgent 사용 시 필요

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;

    protected MonsterSpawner _monsterPool = default;

    private Transform playerTransform;
    private NavMeshAgent navAgent; // NavMeshAgent 컴포넌트

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null )
        {
            Debug.LogError("Monster has no nav mesh!");
        }
    }

    public void InitMonster(MonsterSpawner monsterPool)
    {
        _monsterPool = monsterPool;
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
            _monsterPool.ReturnMonster(this.gameObject);
        }
    }

    public float AttackDamage()
    {
        return _monsterStat.AttackDamage;
    }
}
