using UnityEngine;

public class ShieldCompanionMoveRun : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveRun(CompanionBase companionBase) : base(companionBase)
    {
        companion = companionBase as ShieldCompanion;
    }

    public override void OnEnter() { }

    public override void Move()
    {
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
                    companion.ChangeAttack();    
                }
                return;
            }
            companion.MoveStopToTarget();
            companion.ChangeIdle();
        }
    }

    public override void OnExit() { }
}