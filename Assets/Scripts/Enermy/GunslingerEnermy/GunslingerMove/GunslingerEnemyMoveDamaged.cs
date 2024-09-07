

public class GunslingerEnemyMoveDamaged : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDamaged(Enemy enemy) : base(enemy)
    {
        this.enemy = enemy as GunslingerEnemy;
    }

    public override void Move()
    {
        PlayerWeaponController pwc = GameManager.Instance.PlayerWeaponController;
        float remainHealth = enemy.Damaged(pwc.damage);

        enemy.DamagedEnd(remainHealth);
    }
}
