

public class GunslingerEnemyMoveDamaged : IEnemyMove
{
    private GunslingerEnemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }
    
    public void Move()
    {
        PlayerWeaponController pwc = GameManager.Instance.PlayerWeaponController;
        float remainHealth = enemy.Damaged(pwc.damage);

        enemy.DamagedEnd(remainHealth);
    }
}
