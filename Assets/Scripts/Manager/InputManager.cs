using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
#region Events

    public delegate void MoveInput(Vector2 position, float time);
    public event MoveInput OnMoveInput;
    
    public delegate void MoveCanceledInput(Vector2 position, float time);
    public event MoveCanceledInput OnMoveCanceledInput;
    
    public delegate void AttackInput(bool attack, float time);
    public event AttackInput OnAttackInput;
    
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
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
    }

    private void Start()
    {
        playerAction.Player.Move.performed += PerformedMove;
        playerAction.Player.Move.canceled += CanceledMove;
        playerAction.Player.Attack.performed += PerformedAttack;
    }

    private void PerformedMove(InputAction.CallbackContext context)
    {
        if (OnMoveInput is not null)
        {
            OnMoveInput(playerAction.Player.Move.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void CanceledMove(InputAction.CallbackContext context)
    {
        if (OnMoveCanceledInput is not null)
        {
            OnMoveCanceledInput(playerAction.Player.Move.ReadValue<Vector2>(), (float)context.time);
        }
    }
    
    private void PerformedAttack(InputAction.CallbackContext context)
    {
        if (OnAttackInput is not null)
        {
            OnAttackInput(playerAction.Player.Attack.ReadValue<bool>(), (float)context.time);
        }
    }
}
