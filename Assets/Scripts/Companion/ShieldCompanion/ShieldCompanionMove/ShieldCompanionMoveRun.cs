
public class ShieldCompanionMoveRun : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveRun(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        if (companion.CanAttackTarget())
        {
            companion.MoveStopToTarget();
            companion.CState = CompanionState.Attack;
            companion.ChangeCureMove(companion.moveAttack);
            return;
        }
        
        companion.MoveToTarget();
    }
}
