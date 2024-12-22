using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float attackRange = 2.5f;
    public int health = 20;
    public float teleportCooldown = 10.0f;

    private Transform playerTransform;
    private float teleportTimer = 0.0f;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        teleportTimer -= Time.deltaTime;
        MoveTowardsPlayer();
        TeleportIfFar();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > attackRange)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void TeleportIfFar()
    {
        if (teleportTimer > 0 || playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > 10.0f)
        {
            transform.position = playerTransform.position + (Vector3.right * 2.0f); // 플레이어 근처로 이동
            teleportTimer = teleportCooldown;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
