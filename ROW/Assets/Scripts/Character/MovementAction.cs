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
            Debug.Log("�ȱ� �ִϸ��̼� ");
        }
            
    }

    private void GroundGravity()
    {
        _player.movementVector.y = VERTICAL_GRAVITY;
    }

    private void Rotate()
    {
        // �÷��̾� ���̿� ������ y ��ġ�� ��� ����
        Plane plane = new Plane(Vector3.up, _player.transform.position);

        // ī�޶󿡼� ���콺 ��ġ���� Ray ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ray�� ���� �����ϴ� ���� ���
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // ������Ʈ ��ġ �� ���� ����
            Vector3 direction = (mouseWorldPosition - _player.transform.position).normalized;
            transform.LookAt(mouseWorldPosition);
        }
    }
}
