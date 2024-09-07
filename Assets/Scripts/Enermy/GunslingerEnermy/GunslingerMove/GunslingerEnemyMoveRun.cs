

public class GunslingerEnemyMoveRun : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveRun(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }

    public override void Move()
    {
        if (enemy.CanAttackTarget())
        {
            enemy.MoveStopToTarget();
            enemy.EState = EnemyState.Attack;
            enemy.ChangeCureMove(enemy.moveAttack);
            return;
        }
        
        enemy.MoveToTarget();
    }
}
