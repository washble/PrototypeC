
using UnityEngine;

public class ShieldCompanionMoveRun : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveRun(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Move;
        
        if (companion.CheckFarFromPlayer(10))
        {
            companion.ChangeTargetToPlayer();
            companion.MoveStartToTarget();
            companion.MoveToTarget();
        }
        else
        {
            (int enemyHitCount, RaycastHit[] raycastEnemyHits) = CompanionManager.Instance.RaycastEnemyHits;
            if (enemyHitCount > 0)
            {
                if (!companion.CanAttackTarget())
                {
                    companion.MoveStartToTarget();
                    companion.MoveToTarget();
                }
                else
                {
                    companion.MoveStopToTarget();
                    companion.ChangeAttackTarget(raycastEnemyHits[0].transform);
                    companion.ChangeCurMove(companion.moveAttack);    
                }
                return;
            }
            companion.MoveStopToTarget();
            companion.ChangeCurMove(companion.moveIdle);
        }
    }
}
