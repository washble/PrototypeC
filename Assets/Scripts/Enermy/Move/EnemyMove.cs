
public abstract class EnemyMove : IMove
{
    protected EnemyMove(EnemyBase enemyBase) { }
    public abstract void OnEnter();
    public abstract void Move();
    public abstract void OnExit();
}