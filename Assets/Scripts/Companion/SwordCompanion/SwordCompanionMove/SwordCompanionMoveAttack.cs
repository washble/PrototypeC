public class SwordCompanionMoveAttack : CompanionMove
{
    private SwordCompanion companion;
    
    public SwordCompanionMoveAttack(CompanionBase companionBase) : base(companionBase)
    {
        companion = companionBase as SwordCompanion;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        if (companion.CheckFarFromPlayer(15))
        {
            companion.MoveStartToTarget();
            companion.ChangeRun();
            return;
        }
        
        companion.LookAtTarget();
        companion.Cweapon.AttackStart(0);
    }

    public override void OnExit() { }
}