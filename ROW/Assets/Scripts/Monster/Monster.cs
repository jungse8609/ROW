using UnityEngine;
using UnityEngine.AI; // NavMeshAgent ��� �� �ʿ�

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;
    [SerializeField] protected ObjectPoolManagerSO _monsterPool = default;
    [SerializeField] protected ObjectPoolManagerSO _droppablePool = default;

    protected ObjectPoolManagerSO _dropPool;
    protected Transform playerTransform;
    protected NavMeshAgent navAgent; // NavMeshAgent ������Ʈ

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
        // Player �±׸� ���� ������Ʈ ã��
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        // NavMeshAgent �⺻ ����
        if (navAgent != null)
        {
            // �̵� �ӵ�, ���� �Ÿ� ����
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
