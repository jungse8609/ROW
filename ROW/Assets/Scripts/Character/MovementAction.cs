using UnityEngine;

public class MovementAction : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat;
    [SerializeField] private GameObject _gunObject;

    private Player _player;
    private Animator _anim;
    private CharacterController _characterController;
    private const float VERTICAL_GRAVITY = -5f;
    private float _turnSmoothSpeed;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    public void ReplaceGun(Gun newGun)
    {
        _gunObject = newGun.gameObject;
    }

    private void Update()
    {
        OnUpdate();
        Move();
        GroundGravity();
    }

    private void OnUpdate()
    {
        Move();
        GroundGravity();
        Rotate();
    }

    private void Move()
    {
        _player.movementVector.x = _player.movementInput.x * _playerStat.MoveSpeed;
        _player.movementVector.z = _player.movementInput.z * _playerStat.MoveSpeed;

        _characterController.Move(_player.movementVector * Time.deltaTime);
        _player.movementVector = _characterController.velocity;

        if (_player.movementVector.sqrMagnitude <= 0.02f)
            _anim.SetBool("isWalking", false);
        else
        {
            _anim.SetBool("isWalking", true);
        }
            
    }

    private void GroundGravity()
    {
        _player.movementVector.y = VERTICAL_GRAVITY;
    }

    private void Rotate()
    {
        Vector3 horizontalMovement = _player.movementVector;
        horizontalMovement.y = 0;

        if (horizontalMovement.sqrMagnitude >= 0.02f)
        {
            float targetRotation = Mathf.Atan2(-horizontalMovement.z, horizontalMovement.x) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up *
                Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation + 90.0f, ref _turnSmoothSpeed, 0.2f);
        }

        // 플레이어 높이와 동일한 y 위치의 평면 생성
        Plane plane = new Plane(Vector3.up, _player.transform.position);

        // 카메라에서 마우스 위치로의 Ray 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ray가 평면과 교차하는 지점 계산
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);
            Vector3 direction = (mouseWorldPosition - _gunObject.transform.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _gunObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
