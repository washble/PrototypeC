

public class ShieldCompanionMoveAttack : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveAttack(CompanionBase companionBase) : base(companionBase)
    {
        this.companion = companionBase as ShieldCompanion;
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
