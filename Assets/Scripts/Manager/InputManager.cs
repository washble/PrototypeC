using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
#region Events

#if UNITY_STANDALONE
    public delegate void MovePerformedInput(Vector2 position, float time);
    public event MovePerformedInput OnMovePerformedInput;
    public delegate void MoveCanceledInput(Vector2 position, float time);
    public event MoveCanceledInput OnMoveCanceledInput;
    public delegate void RunPerformedInput(float run, float time);
    public event RunPerformedInput OnRunPerformedInput;
    public delegate void RunCanceledInput(float run, float time);
    public event RunCanceledInput OnRunCanceledInput;
    public delegate void AttackPerformedInput(float attack, float time);
    public event AttackPerformedInput OnAttackPerformedInput;
#endif
    
#if UNITY_ANDROID
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    public delegate void MoveTouch(Vector2 position, float time);
    public event MoveTouch OnMoveTouch;
#endif
    
#endregion

    private PlayerAction playerAction;

    protected override void Awake()
    {
        base.Awake();
        
        playerAction = new PlayerAction();
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();
        
        playerAction.Player.Move.performed += PerformedMove;
        playerAction.Player.Move.canceled += CanceledMove;
        playerAction.Player.Run.performed += PerformedRun;
        playerAction.Player.Attack.performed += PerformedAttack;
        
#if UNITY_ANDROID
        playerAction.Player.PrimaryContact.started += StartTouchPrimary;
        playerAction.Player.PrimaryContact.canceled += EndTouchPrimary;
        playerAction.Player.Move.performed += PerformedTouchPrimary;
#endif
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
        
        playerAction.Player.Move.performed -= PerformedMove;
        playerAction.Player.Move.canceled -= CanceledMove;
        playerAction.Player.Run.performed -= PerformedRun;
        playerAction.Player.Attack.performed -= PerformedAttack;
        
#if UNITY_ANDROID
        playerAction.Player.PrimaryContact.started -= StartTouchPrimary;
        playerAction.Player.PrimaryContact.canceled -= EndTouchPrimary;
        playerAction.Player.Move.performed -= PerformedTouchPrimary;
#endif
    }

    private void PerformedMove(InputAction.CallbackContext context)
    {
        if (OnMovePerformedInput is not null)
        {
            OnMovePerformedInput(playerAction.Player.Move.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void CanceledMove(InputAction.CallbackContext context)
    {
        if (OnMoveCanceledInput is not null)
        {
            OnMoveCanceledInput(playerAction.Player.Move.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void PerformedRun(InputAction.CallbackContext context)
    {
        if (OnRunPerformedInput is not null)
        {
            OnRunPerformedInput(playerAction.Player.Run.ReadValue<float>(), (float)context.time);
        }
    }
    
    private void PerformedAttack(InputAction.CallbackContext context)
    {
        if (OnAttackPerformedInput is not null)
        {
            OnAttackPerformedInput(playerAction.Player.Attack.ReadValue<float>(), (float)context.time);
        }
    }
    
#if UNITY_ANDROID
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch is not null)
        {
            OnStartTouch(playerAction.Player.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch is not null)
        {
            OnEndTouch(playerAction.Player.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void PerformedTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnMoveTouch is not null)
        {
            OnMoveTouch(playerAction.Player.Move.ReadValue<Vector2>(), (float)context.time);
        }   
    }
#endif
}
