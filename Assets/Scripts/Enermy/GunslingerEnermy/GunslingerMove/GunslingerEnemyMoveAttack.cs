

public class GunslingerEnemyMoveAttack : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveAttack(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
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
