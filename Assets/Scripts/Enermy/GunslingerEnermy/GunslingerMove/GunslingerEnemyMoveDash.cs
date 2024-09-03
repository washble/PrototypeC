

public class GunslingerEnemyMoveDash : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public void Move()
    {
        
    }
}
