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

     private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 "Bullet" 태그를 가진 경우
        if (other.CompareTag("Bullet"))
        {
            // 총알에 맞았을 때 데미지를 받음
            TakeDamage(1);  // 총알이 1의 데미지를 준다고 가정 (필요시 수정)
            Destroy(other.gameObject);  // 충돌한 총알을 삭제
        }
    }
}
