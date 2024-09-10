
public class ShieldCompanionMoveAttack : CompanionMove
{
    private ShieldCompanion companion;
    
    public ShieldCompanionMoveAttack(Companion companion) : base(companion)
    {
        this.companion = companion as ShieldCompanion;
    }

    public override void Move()
    {
        
    }
}
