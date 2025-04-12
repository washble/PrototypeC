

public class GunslingerEnemyMoveIdle : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveIdle(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }
    
    public override void Move()
    {
        enemy.EState = EnemyState.Idle;
        
        enemy.MoveStartToTarget();
        enemy.ChangeCurMove(enemy.moveRun);
    }
}
