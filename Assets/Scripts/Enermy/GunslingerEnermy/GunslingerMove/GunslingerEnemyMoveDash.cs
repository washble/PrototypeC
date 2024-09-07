

public class GunslingerEnemyMoveDash : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDash(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }

    public override void Move()
    {
        
    }
}
