using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
#region Events

    public delegate void MovePerformedInput(Vector2 position, float time);
    public event MovePerformedInput OnMovePerformedInput;
    
    public delegate void MoveCanceledInput(Vector2 position, float time);
    public event MoveCanceledInput OnMoveCanceledInput;
    
    public delegate void RunPerformedInput(float run, float time);
    public event RunPerformedInput OnRunPerformedInput;
    
    public delegate void RundCanceledInput(float run, float time);
    public event RundCanceledInput OnRundCanceledInput;
    
    public delegate void AttackPerformedInput(float attack, float time);
    public event AttackPerformedInput OnAttackPerformedInput;
    
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
        playerAction.Player.Run.performed += PerformedRun;
        playerAction.Player.Attack.performed += PerformedAttack;
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
}
