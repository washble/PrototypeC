

public class GunslingerEnemyMoveIdle : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveIdle(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public override void Move()
    {
        enemy.EState = EnemyState.Idle;
        
        enemy.MoveStartToTarget();
        enemy.ChangeCurMove(enemy.moveRun);
    }
}
