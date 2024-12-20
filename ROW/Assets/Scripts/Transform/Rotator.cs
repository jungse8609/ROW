using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 50f; // axis-y rotate speed(angle/sec)

    private void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
