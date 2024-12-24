using UnityEngine;

public abstract class Droppable : MonoBehaviour
{
    public enum DropType { Exp, Weapon };

    [SerializeField] private ObjectPoolManagerSO _pool = default;

    protected DropType type = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEvent(other);
            _pool.ReturnObject(this.gameObject);            
        }
    }

    protected abstract void OnTriggerEvent(Collider other);
}
