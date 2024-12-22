using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 0.0f;
    private float _damage = 10.0f;
    private BulletPoolManager _bulletPool = default;

    public void InitBullet(BulletPoolManager bulletPool, float speed, float damage)
    {
        _speed = speed;
        _damage = damage;
        _bulletPool = bulletPool;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Ѿ��� ���Ϳ� �������� �����Ű�� �κ� (�Ǵ����� �𸣰ڽ��ϴ�)
        // if (other.CompareTag("Monster"))
        // {
        //     var monster = other.GetComponent<Monster>();
        //     if (monster != null)
        //     {
        //         monster.TakeDamage((int)_damage);  // ���Ϳ� ������ ����
        //     }
        // }
        // else if (other.CompareTag("BossMonster"))
        // {
        //     var bossMonster = other.GetComponent<BossMonster>();
        //     if (bossMonster != null)
        //     {
        //         bossMonster.TakeDamage((int)_damage);  // �������Ϳ� ������ ����
        //     }
        // } 

        _bulletPool.ReturnBullet(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        //if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Monster"))
        //{
        //    if (collision.gameObject.CompareTag("Monster"))
        //    {
        //        var monsterHealth = collision.gameObject.GetComponent<MonsterHealth>();
        //        if (monsterHealth != null)
        //        {
        //            monsterHealth.TakeDamage(_damage);
        //        }
        //    }

        //    // �Ѿ� ��Ȱ��ȭ
        //    gameObject.SetActive(false);
        //}
        
    }
}
