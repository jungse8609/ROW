using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ObjectPoolManagerSO _bulletPool = default;

    private float _speed = 0.0f;
    private float _damage = 0.0f;

    public void InitBullet(float speed, float damage)
    {
        _speed = speed;
        _damage = damage;
    }

    private void OnEnable()
    {
        StartCoroutine(TurnOffObject());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator TurnOffObject()
    {
        yield return new WaitForSeconds(1.5f);

        if (gameObject.activeSelf)
        {
            _bulletPool.ReturnObject(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(_damage);
                _bulletPool.ReturnObject(this.gameObject);   
            }
        }
    }
}
