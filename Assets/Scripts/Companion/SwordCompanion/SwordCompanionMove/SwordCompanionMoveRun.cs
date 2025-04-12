using UnityEngine;

public class SwordCompanionMoveRun : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveRun(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as SwordCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Move;
        
        if (companion.CheckFarFromPlayer(10))
        {
            companion.ChangeTargetToPlayer();
            companion.MoveStartToTarget();
            companion.MoveToTarget(6);
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
                    companion.ChangeAttackTarget(raycastEnemyHits[(int)(enemyHitCount * 0.5)].transform);
                    companion.ChangeCurMove(companion.moveAttack);    
                }
                return;
            }
            companion.MoveStopToTarget();
            companion.ChangeCurMove(companion.moveIdle);
        }
    }
}
