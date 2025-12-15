public class GunslingerEnemyMoveDamaged : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDamaged(EnemyBase enemyBase) : base(enemyBase)
    {
        enemy = enemyBase as GunslingerEnemy;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        PlayerWeaponController pwc = GameManager.Instance.PlayerWeaponController;
        float remainHealth = enemy.Damaged(pwc.damage);

        enemy.DamagedEnd(remainHealth);
    }

    public override void OnExit() { }
}