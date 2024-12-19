using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader", order = 0)]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    // Gameplay
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction FireEvent = delegate { };
    public event UnityAction FireCancelEvent = delegate { };
    public event UnityAction ReloadEvent = delegate { };

    // Menues

    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Gameplay.Enable();
        }
    }

    private void OnDisable()
    {
        if (_gameInput != null)
        {
            _gameInput.Gameplay.Disable();
        }

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            FireEvent.Invoke();
        else if (context.phase == InputActionPhase.Canceled)
            FireCancelEvent.Invoke();
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            ReloadEvent.Invoke();
    }
}
