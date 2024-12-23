using UnityEngine;
using UnityEngine.AI; // NavMeshAgent ��� �� �ʿ�

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterStatSO _monsterStat = default;

    private Transform playerTransform;
    private NavMeshAgent navAgent; // NavMeshAgent ������Ʈ

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

    private void Update()
    {
        if (playerTransform == null || navAgent == null) return;

        navAgent.SetDestination(playerTransform.position);
    }

    public void TakeDamage(float damage)
    {
        _monsterStat.CurrentHealth -= damage;
        if (_monsterStat.CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
