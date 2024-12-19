using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private float verticalPull = -5f;
    [SerializeField] private float moveSpeed;

    private Player _player;
    private CharacterController _characterController;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        GroundGravity();
        
        if (_player.fireInput)
        {
            Fire();
            _player.fireInput = false;
        }

        if (_player.reloadInput)
        {
            Reload();
            _player.reloadInput = false;
        }
    }

    private void Move()
    {
        _player.movementVector.x = _player.movementInput.x * moveSpeed;
        _player.movementVector.z = _player.movementInput.z * moveSpeed;

        _characterController.Move(_player.movementVector * Time.deltaTime);
        _player.movementVector = _characterController.velocity;
    }

    private void GroundGravity()
    {
        _player.movementVector.y = verticalPull;
    }

    private void Fire()
    {
        Debug.Log("Fire");
    }

    private void Reload()
    {
        Debug.Log("Reload");
    }
}
