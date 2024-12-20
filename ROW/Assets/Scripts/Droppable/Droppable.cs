using UnityEngine;

public class Droppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("아이템 획득인데 이걸 경ㅇ웨 따라 나눠야 하지 않을까 체력템이나 총으로");
            gameObject.SetActive(false);
        }
    }
}
