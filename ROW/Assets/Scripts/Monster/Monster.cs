using UnityEngine;
using UnityEngine.AI; // NavMeshAgent »ç¿ë ½Ã ÇÊ¿ä

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterStatSO _monsterStat = default;

    private Transform playerTransform;
    private NavMeshAgent navAgent; // NavMeshAgent ÄÄÆ÷³ÍÆ®

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
        // Player ÅÂ±×¸¦ °¡Áø ¿ÀºêÁ§Æ® Ã£±â
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        // NavMeshAgent ±âº» ¼¼ÆÃ
        if (navAgent != null)
        {
            // ÀÌµ¿ ¼Óµµ, ¸ØÃâ °Å¸® ¼³Á¤
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
        // ì¶©ëŒí•œ ê°ì²´ê°€ "Bullet" íƒœê·¸ë¥¼ ê°€ì§„ ê²½ìš°
        if (other.CompareTag("Bullet"))
        {
            // ì´ì•Œì— ë§žì•˜ì„ ë•Œ ë°ë¯¸ì§€ë¥¼ ë°›ìŒ
            TakeDamage(1);  // ì´ì•Œì´ 1ì˜ ë°ë¯¸ì§€ë¥¼ ì¤€ë‹¤ê³  ê°€ì • (í•„ìš”ì‹œ ìˆ˜ì •)
            Destroy(other.gameObject);  // ì¶©ëŒí•œ ì´ì•Œì„ ì‚­ì œ
        }
    }
}
