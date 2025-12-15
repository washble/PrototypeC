public class SwordCompanion : CompanionBase
{
    private IMove moveIdle;
    private IMove moveRun;
    private IMove moveAttack;
    private IMove moveDamaged;
    private IMove moveDie;
    
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

        ChangeState(moveIdle);
    }

    private void ChangeState(IMove newState)
    {
        if(curMove == newState) { return; }
        
        curMove?.OnExit();
        curMove = newState;
        curMove.OnEnter();
    }

    internal void ChangeIdle()
    {
        ChangeState(moveIdle);
    }

    internal void ChangeRun()
    {
        ChangeState(moveRun);
    }
    
    internal void ChangeAttack()
    {
        ChangeState(moveAttack);
    }
}