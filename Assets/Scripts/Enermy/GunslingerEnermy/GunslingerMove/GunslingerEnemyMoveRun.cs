

public class GunslingerEnemyMoveRun : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveRun(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }

    public override void Move()
    {
        enemy.EState = EnemyState.Move;
        
        if (enemy.CanAttackTarget())
        {
            enemy.MoveStopToTarget();
            enemy.ChangeCurMove(enemy.moveAttack);
            return;
        }
        
        enemy.MoveToTarget();
    }
}
