

public class GunslingerEnemyMoveDie : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDie(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }

    public override void Move()
    {
        Die();
    }

    private void Die()
    {
        enemy.EState = EnemyState.Die;
        
        // Temp Setting
        enemy.gameObject.SetActive(false);
    }
}
