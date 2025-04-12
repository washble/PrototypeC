

public class SwordCompanion : CompanionBase
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
        moveIdle = new SwordCompanionMoveIdle(this);
        moveRun = new SwordCompanionMoveRun(this);
        moveAttack = new SwordCompanionMoveAttack(this);

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
