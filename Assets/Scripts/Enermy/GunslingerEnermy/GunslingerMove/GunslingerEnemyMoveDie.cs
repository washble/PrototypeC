

public class GunslingerEnemyMoveDie : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public void Move()
    {
        Die();
    }

    private void Die()
    {
        // Temp Setting
        enemy.gameObject.SetActive(false);
    }
}
