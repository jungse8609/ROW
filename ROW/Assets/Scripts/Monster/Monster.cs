using UnityEngine;
using UnityEngine.AI; // NavMeshAgent ��� �� �ʿ�

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterStatSO _monsterStat = default;

    protected MonsterPoolManager _monsterPool = default;

    private Transform playerTransform;
    private NavMeshAgent navAgent; // NavMeshAgent ������Ʈ

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
    /// ���͸� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void InitMonster()
    {
        // Ǯ �̸��� �±׳� �ٸ� ������� ������ �� �ֽ��ϴ�.
        if (gameObject.CompareTag("Boss"))
        {
            _poolName = "BossPool";
        }
        else
        {
            _poolName = "MonsterPool";
        }

        // �߰� �ʱ�ȭ ������ �ʿ��ϴٸ� ���⿡ �ۼ�
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
        // ���͸� ��Ȱ��ȭ�ϰ� Ǯ�� ��ȯ
        ObjectPoolManager.Instance.ReturnObject(_poolName, gameObject);
    }
}
