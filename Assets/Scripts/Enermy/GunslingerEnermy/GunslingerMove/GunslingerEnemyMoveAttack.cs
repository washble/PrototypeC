

public class GunslingerEnemyMoveAttack : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public void Move()
    {
        if (!enemy.CanAttackTarget())
        {
            enemy.MoveStartToTarget();
            enemy.EState = EnemyState.Move;
            enemy.curMove = enemy.moveRun;
            return;
        }
        
        enemy.LookAtTarget();
        enemy.Eweapon.AttackStart(0);
    }
}
