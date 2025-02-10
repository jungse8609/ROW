using UnityEngine;
using UnityEngine.AI; // NavMeshAgent ��� �� �ʿ�

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;
    [SerializeField] protected ObjectPoolManagerSO _monsterPool = default;
    [SerializeField] protected ObjectPoolManagerSO _droppablePool = default;

    protected ObjectPoolManagerSO _dropPool;
    protected Transform playerTransform;
    protected NavMeshAgent navAgent;

    public float _currentHp;

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
        _currentHp = _monsterStat.MaxHealth;
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        if (navAgent != null)
        {
            navAgent.speed = _monsterStat.MoveSpeed;
            navAgent.stoppingDistance = _monsterStat.AttackRange;
        }
    }

    protected virtual void Update()
    {
        if (playerTransform == null || navAgent == null) return;

        if (Time.timeScale == 0f)
        {
            navAgent.ResetPath();
            navAgent.isStopped = true;
            navAgent.velocity = Vector3.zero;
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
        _currentHp -= damage;
        
        if (_currentHp <= 0)
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
