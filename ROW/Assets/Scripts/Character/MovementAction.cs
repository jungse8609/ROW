using UnityEngine;

public class MovementAction : MonoBehaviour
{
    [SerializeField] private PlayerStatSO _playerStat;

    private Player _player;
    private Animator _anim;
    private CharacterController _characterController;
    private const float VERTICAL_GRAVITY = -5f;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
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
            Debug.Log("걷기 애니메이션 ");
        }
            
    }

    private void GroundGravity()
    {
        _player.movementVector.y = VERTICAL_GRAVITY;
    }

    private void Rotate()
    {
        // 플레이어 높이와 동일한 y 위치의 평면 생성
        Plane plane = new Plane(Vector3.up, _player.transform.position);

        // 카메라에서 마우스 위치로의 Ray 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ray가 평면과 교차하는 지점 계산
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // 오브젝트 위치 및 방향 조정
            Vector3 direction = (mouseWorldPosition - _player.transform.position).normalized;
            transform.LookAt(mouseWorldPosition);
        }
    }
}
