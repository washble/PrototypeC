public class GunslingerEnemyMoveIdle : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveIdle(EnemyBase enemyBase) : base(enemyBase)
    {
        enemy = enemyBase as GunslingerEnemy;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        enemy.MoveStartToTarget();
        enemy.ChangeRun();
    }

    public override void OnExit() { }
}