

public class GunslingerEnemyMoveAttack : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveAttack(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }
    
    public override void Move()
    {
        enemy.EState = EnemyState.Attack;
        
        if (!enemy.CanAttackTarget())
        {
            enemy.MoveStartToTarget();
            enemy.ChangeCurMove(enemy.moveRun);
            return;
        }
        
        enemy.LookAtTarget();
        enemy.Eweapon.AttackStart(0);
    }
}
