public class GunslingerEnemyMoveDash : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDash(EnemyBase enemyBase) : base(enemyBase)
    {
        enemy = enemyBase as GunslingerEnemy;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        
    }

    public override void OnExit() { }
}