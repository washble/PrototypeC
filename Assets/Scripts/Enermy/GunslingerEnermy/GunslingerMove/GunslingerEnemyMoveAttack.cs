

public class GunslingerEnemyMoveAttack : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveAttack(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public override void Move()
    {
        if (!enemy.CanAttackTarget())
        {
            enemy.MoveStartToTarget();
            enemy.EState = EnemyState.Move;
            enemy.ChangeCureMove(enemy.moveRun);
            return;
        }
        
        enemy.LookAtTarget();
        enemy.Eweapon.AttackStart(0);
    }
}
