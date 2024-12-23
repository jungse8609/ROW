using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float attackRange = 1.5f;
    public int health = 10;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
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
