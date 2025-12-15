using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerWeaponController))]
public class PlayerMoveController : PlayerBase
{
    internal Vector2 direction = Vector2.zero;

    [Header("[Move]")]
    [SerializeField] internal MoveType moveType;
    [SerializeField] internal float lookAtSpeed = 15f;
    [SerializeField] internal float baseSpeed = 10f;
    [SerializeField] internal float addRunSpeed = 5f;
    
    private bool moveHold = false;
    internal bool isRunning = false;
    
    internal enum MoveType
    {
        Absolute,
        Relative,
    }
    
    private IMove curMove;
    private IMove moveIdle;
    private IMove moveWalk;
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
        moveWalk = new PlayerMoveWalk(this);
        moveAttack = new PlayerMoveAttack(this);
        moveDie = new PlayerMoveDie(this);

        StateInit();
    }
    
    private void StateInit()
    {
        direction = Vector2.zero;
        navMeshAgent.speed = baseSpeed;

        ChangeState(moveIdle);
    }
    
    private void ChangeState(IMove newState)
    {
        if(curMove == newState) { return; }
        
        curMove?.OnExit();
        curMove = newState;
        curMove.OnEnter();
    }
    
    private void Update()
    {
        if(moveHold) { return; }
        
        curMove.Move();
    }
    
    internal void MoveHold(bool value)
    {
        moveHold = value;
    }
    
    private void InputMovePerformed(Vector2 position, float time)
    {
        direction = position;
        
        ChangeState(moveWalk);
    }
    
    private void InputMoveCanceled(Vector2 position, float time)
    {
        StateInit();
    }
  
    private void InputRunPerformed(float run, float time)
    {
        if(run < 1) { return; }

        Run();
    }


    private void InputAttackPerformed(float attack, float time)
    {
        if(attack < 1) { return; }

        ChangeState(moveAttack);
    }
    
    private void Run()
    {
        if(isRunning) { return; }
        
        isRunning = true;
        navMeshAgent.speed += addRunSpeed;
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
