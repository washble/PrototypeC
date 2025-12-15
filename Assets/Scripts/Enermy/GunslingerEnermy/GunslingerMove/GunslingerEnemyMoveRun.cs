public class GunslingerEnemyMoveRun : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveRun(EnemyBase enemyBase) : base(enemyBase)
    {
        enemy = enemyBase as GunslingerEnemy;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        if (enemy.CanAttackTarget())
        {
            enemy.MoveStopToTarget();
            enemy.ChangeAttack();
            return;
        }
        
        enemy.MoveToTarget();
    }

    public override void OnExit() { }
}