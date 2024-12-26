using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    // [SerializeField] private AudioClip _hitSoundClip;
    // private AudioPlayer _playerAudio;
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
        // _playerAudio = GetComponent<AudioPlayer>();
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
        if(Time.timeScale !=0)      // 일시정지중 총 안나가게 처리해봄
        {
            fireInput = true;
        }
    }

    private void OnFireCancel()
    {
        fireInput = false;
    }

    private void OnReload()
    {
        reloadInput = true;
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Boss")) // 몬스터와 보스와 충돌했을 때
    //     {
    //         PlayHitSound(); // 피격 사운드 재생
    //     }
    // }
    // private void PlayHitSound()
    // {
    //     if (_hitSoundClip != null)
    //     {
    //         _playerAudio.PlayAudioClip(_hitSoundClip); // 피격 사운드 재생
    //     }
    // }
}
