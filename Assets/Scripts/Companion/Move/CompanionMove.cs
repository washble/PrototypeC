public abstract class CompanionMove : IMove
{
    protected CompanionMove(CompanionBase companionBase) { }
    public abstract void OnEnter();
    public abstract void Move();
    public abstract void OnExit();
}