
public class ShieldCompanionMoveRun : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveRun(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        if (!companion.CheckFarFromPlayer())
        {
            companion.ChangeTargetToPlayer();
        }
        else if (companion.CanAttackTarget())
        {
            companion.MoveStopToTarget();
            companion.CState = CompanionState.Attack;
            companion.ChangeCurMove(companion.moveAttack);
            return;
        }
        
        companion.MoveStartToTarget();
        companion.MoveToTarget();
    }
}
