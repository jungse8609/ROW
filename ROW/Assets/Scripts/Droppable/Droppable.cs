using UnityEngine;

public abstract class Droppable : MonoBehaviour
{
    public enum DropType { Stat, Weapon };

    protected DropType type = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            OnTriggerEvent(other);
        }
    }

    protected abstract void OnTriggerEvent(Collider other);
}
