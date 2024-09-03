

public class GunslingerEnemyMoveRun : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public void Move()
    {
        if (enemy.CanAttackTarget())
        {
            enemy.MoveStopToTarget();
            enemy.EState = EnemyState.Attack;
            enemy.curMove = enemy.moveAttack;
            return;
        }
        
        enemy.MoveToTarget();
    }
}
