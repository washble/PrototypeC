

public class PlayerMoveIdle : IMove
{
    private readonly PlayerMoveController playerMoveController;
    private readonly PlayerAnimationController playerAnimationController;

    public PlayerMoveIdle(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;
    }
    
    public void Move()
    {
        playerMoveController.playerState = PlayerState.Idle;
        
        playerAnimationController.RunEnd();
        playerAnimationController.AttackEnd();
    }
}
