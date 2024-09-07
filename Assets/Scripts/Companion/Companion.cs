using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Companion : MonoBehaviour
{
    /**
     * TODO Move the state storage to the ScriptableObject
     */
    [Header("[Status]")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float health = 30;
    [SerializeField] private float shield = 5;
    [SerializeField] private float damage = 5;
    [SerializeField] private float attackDistance = 5;
    [SerializeField] private float lookAtSpeed = 2f;
    
    [Header("[Weapon]")]
    [SerializeField] private Weapon weapon;
    
    // =========== Component =========== //
    protected CompanionMove CurMove;
    
    private Transform target;
    protected Transform CTarget => target;
    
    private Animator animator;
    protected Animator CAnimator => animator;

    private NavMeshAgent navMeshAgent;
    protected NavMeshAgent CNavMeshAgent => navMeshAgent;

    internal CompanionState CState;
        
    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    
    protected virtual void Start()
    {
        NavMeshInitSettings();
        SetDamage(damage);

        //WeaponInitSettings();
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
        if (!weapon) { return; }
        
        weapon.SetWeaponGrabPosition(default, default, default);
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
        Vector3 offset = target.position - transform.position;
        if (offset.sqrMagnitude < attackDistance * attackDistance)
        {
            return true;
        }
        return false;
    }
}

public enum CompanionState
{
    Idle,
    Attack,
    Move,
    Die,
}