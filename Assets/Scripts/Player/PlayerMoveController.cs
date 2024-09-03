using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerWeaponController))]
public class PlayerMoveController : Singleton<PlayerMoveController>
{
    private InputManager inputManager;
    private UIManager uiManager;
    private EnemySpawner enemySpawner;

    internal PlayerWeaponController playerWeaponController;
    internal NavMeshAgent navMeshAgent;
    private Transform thisTransform;
    internal Vector2 direction = Vector2.zero;

    internal PlayerState playerState { get; set; } = PlayerState.Idle;

    [Header("[Move]")]
    [SerializeField] internal float lookAtSpeed = 15f;

    internal RaycastHit[] enemyRaycastHit;
    internal int hitEnemyCount;
    private LayerMask enemyLayerMask;

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

    protected override void Awake()
    {
        base.Awake();
        
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;
        enemySpawner = EnemySpawner.Instance;

        playerWeaponController = GetComponent<PlayerWeaponController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        inputManager.OnMoveInput += InputMove;
        inputManager.OnMoveCanceledInput += InputMoveCanceled;
        inputManager.OnAttackInput += InputAttack;
    }

    private void OnDisable()
    {
        inputManager.OnMoveInput -= InputMove;
        inputManager.OnMoveCanceledInput -= InputMoveCanceled;
        inputManager.OnAttackInput -= InputAttack;
    }
    
    private void Start()
    {
        thisTransform = transform;
        
        MoveSettings();
        AttackSettings();
    }
    
    private void Update()
    {
        curMove.Move();
        CheckAttackRadiusEnemy();
    }

    private void MoveSettings()
    {
        moveIdle = new PlayerMoveIdle(this);
        moveRun = new PlayerMoveRun(this);
        moveAttack = new PlayerMoveAttack(this);
        moveDie = new PlayerMoveDie(this);

        StateInit();
    }

    private void AttackSettings()
    {
        enemyRaycastHit = new RaycastHit[enemySpawner.Enemies.Length];
        
        enemyLayerMask = LayerMask.GetMask(GameObjectLayer.Enemy.ToString());
    }
    
    private void StateInit()
    {
        direction = Vector2.zero;
        
        curMove = moveIdle;
    }
    
    private void InputMove(Vector2 position, float time)
    {
        direction = position;
        curMove = moveRun;
    }
    
    private void InputMoveCanceled(Vector2 position, float time)
    {
        StateInit();
    }

    private void InputAttack(bool attack, float time)
    {
        if(!attack) { return; }
        
        curMove = moveAttack;
    }

    internal void AttackEnd()
    {
        StateInit();
    }
    
    private void CheckAttackRadiusEnemy()
    {
        if(curMove != moveIdle) return;
        
        hitEnemyCount = Physics.SphereCastNonAlloc(
            thisTransform.position, playerWeaponController.attackDistance, 
            thisTransform.forward, 
            enemyRaycastHit, 0, enemyLayerMask, QueryTriggerInteraction.Collide);
        
        if (hitEnemyCount > 0)
        {
            curMove = moveAttack;  
        }
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
