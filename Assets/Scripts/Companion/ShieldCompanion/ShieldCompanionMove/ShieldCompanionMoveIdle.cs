public class ShieldCompanionMoveIdle : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveIdle(CompanionBase companionBase) : base(companionBase)
    {
        companion = companionBase as ShieldCompanion;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        companion.ChangeRun();
    }

    public override void OnExit() { }
}