public class GunslingerEnemyMoveAttack : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveAttack(EnemyBase enemyBase) : base(enemyBase)
    {
        enemy = enemyBase as GunslingerEnemy;
    }
    
    public override void OnEnter() { }
    
    public override void Move()
    {
        if (!enemy.CanAttackTarget())
        {
            enemy.MoveStartToTarget();
            enemy.ChangeRun();
            return;
        }
        
        enemy.LookAtTarget();
        enemy.Eweapon.AttackStart(0);
    }
    
    public override void OnExit() { }
}