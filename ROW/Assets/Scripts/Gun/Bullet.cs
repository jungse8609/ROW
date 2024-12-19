using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _damage = 2.0f;
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
        Debug.Log("충돌해서 총알 반환");
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
