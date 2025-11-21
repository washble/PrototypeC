using UnityEngine;

public class PlayerMoveAttack : PlayerMove
{
    private readonly PlayerWeaponController playerWeaponController;

    private readonly Transform thisTransform;
    private Transform selectTargetTransform;
    
    // Calibrate according to the animation
    private readonly Quaternion calibrateAttackQuaternion = Quaternion.Euler(0, 45,  0);

    public PlayerMoveAttack(PlayerMoveController playerMoveController) : base(playerMoveController)
    {
        playerWeaponController = playerMoveController.playerWeaponController;

        thisTransform = playerMoveController.transform;
    }
    
    public override void Move()
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
