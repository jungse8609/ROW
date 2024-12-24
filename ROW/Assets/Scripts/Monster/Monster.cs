using UnityEngine;
using UnityEngine.AI; // NavMeshAgent 사용 시 필요

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;
    [SerializeField] protected ObjectPoolManagerSO _monsterPool = default;
    [SerializeField] protected ObjectPoolManagerSO _droppablePool = default;

    protected ObjectPoolManagerSO _dropPool;
    protected Transform playerTransform;
    protected NavMeshAgent navAgent; // NavMeshAgent 컴포넌트

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null )
        {
            Debug.LogError("Monster has no nav mesh!");
        }
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

        if (Time.timeScale == 0f)
        {
            navAgent.isStopped = true;
            return;
        }
        else
        {
            navAgent.isStopped = false;
        }

        navAgent.SetDestination(playerTransform.position);
    }

    public virtual void TakeDamage(float damage)
    {
        _monsterStat.CurrentHealth -= damage;
        if (_monsterStat.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public float AttackDamage()
    {
        return _monsterStat.AttackDamage;
    }

    protected virtual void Die()
    {
        _monsterPool.ReturnObject(this.gameObject);

        GameObject drop = _droppablePool.GetObject();
        drop.transform.position = transform.position;
    }
}
