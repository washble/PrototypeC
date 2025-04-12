

public class ShieldCompanionMoveIdle : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveIdle(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as ShieldCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Idle;
        companion.ChangeCurMove(companion.moveRun);
    }
}
