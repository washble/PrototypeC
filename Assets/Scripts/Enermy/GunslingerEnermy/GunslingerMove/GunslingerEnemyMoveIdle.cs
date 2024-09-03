

public class GunslingerEnemyMoveIdle : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }

    public void Move()
    {
        enemy.MoveStartToTarget();
        enemy.EState = EnemyState.Move;
        enemy.curMove = enemy.moveRun;
    }
}
