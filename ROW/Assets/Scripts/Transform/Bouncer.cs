using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [Header("Bouncing Settings")]
    [SerializeField] private float _bounceAmplitude = 0.1f;
    [SerializeField] private float _bounceFrequency = 2f;

    private float _initialY;

    private void Start()
    {
        _initialY = transform.position.y;
    }

    private void Update()
    {
        float newY = _initialY + Mathf.Sin(Time.time * _bounceFrequency) * _bounceAmplitude;
        Vector3 newPosition = transform.position;
        newPosition.y = newY;
        transform.position = newPosition;
    }
}
