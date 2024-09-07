

public class GunslingerEnemyMoveDie : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDie(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }

    public override void Move()
    {
        Die();
    }

    private void Die()
    {
        // Temp Setting
        enemy.gameObject.SetActive(false);
    }
}
