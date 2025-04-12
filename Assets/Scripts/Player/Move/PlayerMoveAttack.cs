using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveAttack : IMove
{
    private readonly PlayerMoveController playerMoveController;
    private readonly PlayerAnimationController playerAnimationController;
    private readonly PlayerWeaponController playerWeaponController;
    private readonly NavMeshAgent navMeshAgent;

    private readonly Transform thisTransform;
    private Transform selectTargetTransform;
    
    // Calibrate according to the animation
    private readonly Quaternion calibrateAttackQuaternion = Quaternion.Euler(0, 45,  0);

    public PlayerMoveAttack(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;
        playerWeaponController = playerMoveController.playerWeaponController;

        thisTransform = playerMoveController.transform;
        navMeshAgent = playerMoveController.navMeshAgent;
    }
    
    public void Move()
    {
        playerMoveController.playerState = PlayerState.Attack;
        MoveAnimation();
        playerWeaponController.WeaponBase.AttackStart(0);
        playerMoveController.AttackEnd();
    }

    private void LookAtTarget(Transform targetTransform)
    {
        Vector3 curDirection = targetTransform.position - thisTransform.position;
        Vector3 scaledMovement = new Vector3(curDirection.x, 0, curDirection.z).normalized;
        Vector3 calibrateDirection = calibrateAttackQuaternion * scaledMovement;
        navMeshAgent.transform.LookAt(navMeshAgent.transform.position + calibrateDirection, Vector3.up);
    }

    private void MoveAnimation()
    {
        playerAnimationController.AttackStart();
    }
}
