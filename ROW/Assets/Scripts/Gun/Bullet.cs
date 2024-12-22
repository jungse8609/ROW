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
        // 총알이 몬스터에 데미지를 적용시키는 부분 (되는지는 모르겠습니다)
        // if (other.CompareTag("Monster"))
        // {
        //     var monster = other.GetComponent<Monster>();
        //     if (monster != null)
        //     {
        //         monster.TakeDamage((int)_damage);  // 몬스터에 데미지 적용
        //     }
        // }
        // else if (other.CompareTag("BossMonster"))
        // {
        //     var bossMonster = other.GetComponent<BossMonster>();
        //     if (bossMonster != null)
        //     {
        //         bossMonster.TakeDamage((int)_damage);  // 보스몬스터에 데미지 적용
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

        //    // 총알 비활성화
        //    gameObject.SetActive(false);
        //}
        
    }
}
