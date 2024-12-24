using UnityEngine;

public abstract class Droppable : MonoBehaviour
{
    public enum DropType { Stat, Weapon };

    [SerializeField] private ObjectPoolManagerSO _pool = default;

    protected DropType type = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pool.ReturnObject(this.gameObject);
            Debug.Log(" ¹ÝÈ¯!!");
            OnTriggerEvent(other);
        }
    }

    protected abstract void OnTriggerEvent(Collider other);
}
