using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerWeaponController))]
public class PlayerMoveController : PlayerBase
{
    internal Vector2 direction = Vector2.zero;

    internal PlayerState playerState = PlayerState.Idle;

    [Header("[Move]")]
    [SerializeField] internal float lookAtSpeed = 15f;

    private IMove curMove;
    private IMove moveIdle;
    private IMove moveRun;
    private IMove moveAttack;
    private IMove moveDie;

#if UNITY_EDITOR
    [Header("[Debug]")] 
    [SerializeField] private bool isDebug = false;
    [SerializeField] private Color attackRadiusColor = new Color(1, 0, 0, 0.5f);
#endif
    
    private void OnEnable()
    {
        inputManager.OnMovePerformedInput += InputMovePerformed;
        inputManager.OnMoveCanceledInput += InputMoveCanceled;
        inputManager.OnRunPerformedInput += InputRunPerformed;
        inputManager.OnAttackPerformedInput += InputAttackPerformed;
    }

    private void OnDisable()
    {
        inputManager.OnMovePerformedInput -= InputMovePerformed;
        inputManager.OnMoveCanceledInput -= InputMoveCanceled;
        inputManager.OnRunPerformedInput -= InputRunPerformed;
        inputManager.OnAttackPerformedInput -= InputAttackPerformed;
    }
    
    private void Start()
    {
        MoveSettings();
    }
    
    private void MoveSettings()
    {
        moveIdle = new PlayerMoveIdle(this);
        moveRun = new PlayerMoveRun(this);
        moveAttack = new PlayerMoveAttack(this);
        moveDie = new PlayerMoveDie(this);

        StateInit();
    }
    
    private void StateInit()
    {
        direction = Vector2.zero;
        (moveRun as PlayerMoveRun)!.StopSpeedRunning();
        
        curMove = moveIdle;
    }
    
    private void Update()
    {
        curMove.Move();
    }
    
    private void InputMovePerformed(Vector2 position, float time)
    {
        direction = position;
        curMove = moveRun;
    }
    
    private void InputMoveCanceled(Vector2 position, float time)
    {
        StateInit();
    }
  
    private void InputRunPerformed(float run, float time)
    {
        if(run < 1) { return; }

        (moveRun as PlayerMoveRun)!.StartSpeedRunning();
    }


    private void InputAttackPerformed(float attack, float time)
    {
        if(attack < 1) { return; }
        
        curMove = moveAttack;
    }

    internal void AttackEnd()
    {
        StateInit();
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!isDebug) { return; }
        if(!playerWeaponController) { playerWeaponController = GetComponent<PlayerWeaponController>(); }
        
        Gizmos.color = attackRadiusColor;
        Gizmos.DrawSphere(transform.position, playerWeaponController.attackDistance);
    }
#endif
}

public enum PlayerState
{
    Idle,
    Run,
    Dash,
    Attack,
    Die,
}
