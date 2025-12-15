public class SwordCompanionMoveIdle : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveIdle(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as SwordCompanion;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        companion.ChangeRun();
    }

    public override void OnExit() { }
}