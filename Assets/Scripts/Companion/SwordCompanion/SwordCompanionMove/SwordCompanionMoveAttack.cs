

public class SwordCompanionMoveAttack : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveAttack(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as SwordCompanion;
    }

    public override void Move()
    {
        companion.CState = CompanionState.Attack;
        if (companion.CheckFarFromPlayer(15))
        {
            companion.MoveStartToTarget();
            companion.ChangeCurMove(companion.moveRun);
            return;
        }
        
        companion.LookAtTarget();
        companion.Cweapon.AttackStart(0);
    }
}
