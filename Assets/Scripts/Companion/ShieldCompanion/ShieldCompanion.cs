

public class ShieldCompanion : CompanionBase
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
        moveAttack = new ShieldCompanionMoveAttack(this);

        InitState();
    }

    private void InitState()
    {
        CurMove = moveIdle;
    }

    internal void ChangeCurMove(CompanionMove move)
    {
        CurMove = move;
    }
}
