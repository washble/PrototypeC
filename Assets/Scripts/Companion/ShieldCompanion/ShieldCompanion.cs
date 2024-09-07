
public class ShieldCompanion : Companion
{
    internal CompanionMove moveIdle;
    internal CompanionMove moveRun;
    internal CompanionMove moveAttack;
    internal CompanionMove moveDamaged;
    internal CompanionMove moveDie;
    
    protected override void Start()
    {
        base.Start();
        
        CompanionMoveSettings();
    }

    private void CompanionMoveSettings()
    {
        moveIdle = new ShieldCompanionMoveIdle(this);
        moveRun = new ShieldCompanionMoveRun(this);

        InitState();
    }

    private void InitState()
    {
        CurMove = moveIdle;
    }

    internal void ChangeCureMove(CompanionMove move)
    {
        CurMove = move;
    }
}
