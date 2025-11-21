public class PlayerMoveDie : PlayerMove
{
    public PlayerMoveDie(PlayerMoveController playerMoveController) : base(playerMoveController)
    {
        
    }
    
    public override void Move()
    {
        playerMoveController.playerState = PlayerState.Die;
    }
}