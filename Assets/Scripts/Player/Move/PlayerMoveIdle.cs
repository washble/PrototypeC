public class PlayerMoveIdle : PlayerMove
{
    public PlayerMoveIdle(PlayerMoveController playerMoveController) : base(playerMoveController)
    {
        
    }
    
    public override void Move()
    {
        playerMoveController.playerState = PlayerState.Idle;
        
        playerAnimationController.RunEnd();
        playerAnimationController.AttackEnd();
    }
}