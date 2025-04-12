

public class GunslingerEnemyMoveDash : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDash(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }

    public override void Move()
    {
        
    }
}
