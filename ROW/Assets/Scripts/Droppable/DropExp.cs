using UnityEngine;

public class DropExp : Droppable
{
    protected override void OnTriggerEvent(Collider other)
    {
        LevelManager levelManager = other.GetComponent<LevelManager>();
        levelManager.GetExp();
    }
}
