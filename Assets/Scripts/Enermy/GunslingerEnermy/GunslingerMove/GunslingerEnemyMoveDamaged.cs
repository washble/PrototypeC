

public class GunslingerEnemyMoveDamaged : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDamaged(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }

    public override void Move()
    {
        PlayerWeaponController pwc = GameManager.Instance.PlayerWeaponController;
        float remainHealth = enemy.Damaged(pwc.damage);

        enemy.DamagedEnd(remainHealth);
    }
}
