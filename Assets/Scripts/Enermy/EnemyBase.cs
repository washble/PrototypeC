using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent), typeof(Collider))]
public abstract class EnemyBase : MonoBehaviour
{
    /**
     * TODO Move the state storage to the ScriptableObject
     */
    [Header("[Status]")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float health = 30;
    [SerializeField] private float shield = 5;
    [SerializeField] private float damage = 5;
    [SerializeField] private float attackDistance = 10;
    [SerializeField] private float lookAtSpeed = 2f;
    
    [FormerlySerializedAs("weapon")]
    [Header("[Weapon]")]
    [SerializeField] private WeaponBase weaponBase;
    internal WeaponBase Eweapon => weaponBase;

    // =========== Component =========== //
    protected EnemyMove CurMove;
    
    private Transform target;
    protected Transform ETarget => target;
    
    private Animator animator;
    protected Animator EAnimator => animator;

    private NavMeshAgent navMeshAgent;
    protected NavMeshAgent ENavMeshAgent => navMeshAgent;

    internal EnemyState EState;
    
    protected virtual void Awake()
    {
        target = GameManager.Instance.Player.transform;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        NavMeshInitSettings();
        SetDamage(damage);

        // WeaponInitSettings();
    }

    protected virtual void Update()
    {
        CurMove.Move();
    }

    private void NavMeshInitSettings()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.autoBraking = false;
        navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    }

    private void WeaponInitSettings()
    {
        if (!weaponBase) { return; }
        
        weaponBase.SetWeaponGrabPosition(default, default, default);
    }
    
    internal virtual void MoveStartToTarget()
    {
        navMeshAgent.isStopped = false;
    }

    internal virtual void MoveStopToTarget()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }
    
    internal virtual void MoveToTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }
    
    protected void SetDamage(float setDamage)
    {
        EnemyManager.Instance.enemyDamage[GameObjectTag.EGunslinger] = setDamage;
    }

    internal virtual float Damaged(float damaged)
    {
        return health -= damaged - shield;
    }

    protected void SpeedChange(float changeAmount)
    {
        navMeshAgent.speed = speed += changeAmount;
    }

    internal void LookAtTarget()
    {
        Vector3 thisPosition = transform.position;
        Vector3 direction = target.position - thisPosition;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime
                                * new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(scaledMovement, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Clamp01(Time.deltaTime * lookAtSpeed));
    }

    internal bool CanAttackTarget()
    {
        return Vector3SqrMagnitudeLessThan
            (
                target.position,
                transform.position,
                attackDistance
            );
    }
    
    private bool Vector3SqrMagnitudeLessThan(Vector3 vector1, Vector3 vector2, float distance)
    {
        return Vector3.SqrMagnitude(vector1 - vector2) < distance * distance;
    }
}

public enum EnemyState
{
    Idle,
    Attack,
    Move,
    Die,
}
