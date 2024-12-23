using UnityEngine;

public class DropGun : Droppable
{
    [SerializeField] private GameObject _gunPrefab;

    protected override void OnTriggerEvent(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject randomGun = Instantiate(_gunPrefab);

            randomGun.transform.parent = other.transform;
        }
    }
}
