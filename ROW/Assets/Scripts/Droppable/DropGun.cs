using UnityEngine;

public class DropGun : Droppable
{
    [SerializeField] private GameObject _gunPrefab;

    protected override void OnTriggerEvent(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunReplacer currentGun = other.GetComponent<GunReplacer>();
            currentGun.ReplaceObject(_gunPrefab);

            Destroy(this.gameObject);
        }
    }
}
