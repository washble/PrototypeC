

public class ShieldCompanionMoveAttack : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveAttack(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
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
