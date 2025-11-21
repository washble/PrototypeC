using UnityEngine.AI;

public abstract class PlayerMove : IMove
{
    protected readonly PlayerMoveController playerMoveController;
    protected readonly PlayerAnimationController playerAnimationController;
    protected readonly NavMeshAgent navMeshAgent;

    protected PlayerMove(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;

        navMeshAgent = playerMoveController.navMeshAgent;
    }
    
    public abstract void Move();
}
