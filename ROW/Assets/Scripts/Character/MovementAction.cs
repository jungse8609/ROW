using UnityEngine;
using UnityEngine.UIElements;

public class MovementAction : MonoBehaviour
{
    [SerializeField] private float verticalPull = -5f;
    [SerializeField] private float moveSpeed = 6.0f;

    private Player _player;
    private CharacterController _characterController;

    private void Awake()
    {
        _player = GetComponent<Player>();
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
        _player.movementVector.x = _player.movementInput.x * moveSpeed;
        _player.movementVector.z = _player.movementInput.z * moveSpeed;

        _characterController.Move(_player.movementVector * Time.deltaTime);
        _player.movementVector = _characterController.velocity;
    }

    private void GroundGravity()
    {
        _player.movementVector.y = verticalPull;
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
