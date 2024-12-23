using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    [HideInInspector] public Vector3 movementInput;
    [HideInInspector] public Vector3 movementVector;
    [HideInInspector] public bool fireInput;
    [HideInInspector] public bool reloadInput;

    private Vector2 _inputVector; // x : x movement, y : z movement
    private float _previousSpeed;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovement;
        _inputReader.FireEvent += OnFire;
        _inputReader.FireCancelEvent += OnFireCancel;
        _inputReader.ReloadEvent += OnReload;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovement;
        _inputReader.FireEvent -= OnFire;
        _inputReader.FireCancelEvent += OnFireCancel;
        _inputReader.ReloadEvent -= OnReload;
    }

    private void Update()
    {
        RecalculateMovement();
    }

    private void RecalculateMovement()
    {
        float targetSpeed;
        Vector3 adjustedMovement;
        
        targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
        targetSpeed = Mathf.Lerp(_previousSpeed, targetSpeed, Time.deltaTime * 4.0f);
        adjustedMovement = new Vector3(_inputVector.x, 0, _inputVector.y);

        movementInput = adjustedMovement * targetSpeed;

        _previousSpeed = targetSpeed;
    }

    /* Event Listener */

    private void OnMovement(Vector2 movement)
    {
        _inputVector = movement;
    }

    private void OnFire()
    {
        fireInput = true;
    }

    private void OnFireCancel()
    {
        fireInput = false;
    }

    private void OnReload()
    {
        reloadInput = true;
    }
}
