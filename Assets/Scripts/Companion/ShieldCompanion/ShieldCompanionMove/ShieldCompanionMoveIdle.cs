

public class ShieldCompanionMoveIdle : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveIdle(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Idle;
        companion.ChangeCurMove(companion.moveRun);
    }
}
