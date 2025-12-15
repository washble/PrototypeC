public class GunslingerEnemyMoveDie : EnemyMove
{
    private GunslingerEnemy enemy;
    
    public GunslingerEnemyMoveDie(EnemyBase enemyBase) : base(enemyBase)
    {
        this.enemy = enemyBase as GunslingerEnemy;
    }

    public override void OnEnter() { }

    public override void Move()
    {
        Die();
    }

    public override void OnExit() { }

    private void Die()
    {
        // Temp Setting
        enemy.gameObject.SetActive(false);
    }
}