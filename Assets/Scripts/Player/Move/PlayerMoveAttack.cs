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
        int reAttackDelayMilliSecond = 0;
        if (IsRetarget())
        {
            selectTargetTransform = SelectTarget();
            reAttackDelayMilliSecond = 250;
        }
        LookAtTarget(selectTargetTransform);
        
        playerMoveController.playerState = PlayerState.Attack;
        MoveAnimation();
        playerWeaponController.Weapon.AttackStart(reAttackDelayMilliSecond);
        playerMoveController.AttackEnd();
    }

    private bool IsRetarget()
    {
        return playerMoveController.playerState != PlayerState.Attack
               || !selectTargetTransform.gameObject.activeInHierarchy;
    }

    private Transform SelectTarget()
    {
        RaycastHit[] raycastHits = playerMoveController.enemyRaycastHit;
        int hitEnemyCount = playerMoveController.hitEnemyCount;
        Vector3 thisPosition = thisTransform.position;

        Transform targetTransform = null;
        float preSqrMagnitude = int.MaxValue;
        for (int i = 0; i < hitEnemyCount; i++)
        {
            RaycastHit curRaycastHit = raycastHits[i];
            Transform curTargetTransform = curRaycastHit.transform;
            Vector3 curDirection = curTargetTransform.position - thisPosition;

            float curSqrMagnitude = curDirection.sqrMagnitude; 
            if (curSqrMagnitude < preSqrMagnitude)
            {
                preSqrMagnitude = curSqrMagnitude;
                targetTransform = curTargetTransform;
            }
        }

        return targetTransform;
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
