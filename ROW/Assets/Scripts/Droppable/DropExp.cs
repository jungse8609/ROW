using UnityEngine;

public class DropExp : Droppable
{
    protected override void OnTriggerEvent(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager levelManager = other.GetComponent<LevelManager>();
            levelManager.GetExp();
            gameObject.SetActive(false);
        }
    }
}
