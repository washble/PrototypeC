public class ShieldCompanionMoveAttack : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveAttack(CompanionBase companionBase) : base(companionBase)
    {
        companion = companionBase as ShieldCompanion;
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