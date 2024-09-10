
public class ShieldCompanionMoveAttack : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveAttack(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        if (!companion.CanAttackTarget())
        {
            companion.MoveStartToTarget();
            companion.CState = CompanionState.Move;
            companion.ChangeCurMove(companion.moveRun);
            return;
        }
        
        companion.LookAtTarget();
        companion.Cweapon.AttackStart(0);
    }
}
