

public class SwordCompanionMoveAttack : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveAttack(Companion companion) : base(companion)
    {
        this.companion = companion as SwordCompanion;
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
