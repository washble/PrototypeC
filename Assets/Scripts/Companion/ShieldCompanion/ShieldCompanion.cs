public class ShieldCompanion : CompanionBase
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
        moveIdle = new ShieldCompanionMoveIdle(this);
        moveRun = new ShieldCompanionMoveRun(this);
        moveAttack = new ShieldCompanionMoveAttack(this);

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