public class PlayerMoveIdle : PlayerMove
{
    public PlayerMoveIdle(PlayerMoveController playerMoveController) : base(playerMoveController)
    {
        
    }

    public override void OnEnter() { }

    public override void Move()
    {
        playerAnimationController.RunEnd();
        playerAnimationController.AttackEnd();
    }

    public override void OnExit() { }
}