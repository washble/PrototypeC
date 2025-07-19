using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveWalk : IMove
{
    private readonly PlayerMoveController playerMoveController;
    private readonly PlayerAnimationController playerAnimationController;
    private readonly NavMeshAgent navMeshAgent;

    public PlayerMoveWalk(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;
        
        navMeshAgent = playerMoveController.navMeshAgent;
        navMeshAgent.updateRotation = false;
    }
    
    public void Move()
    {
        if (playerMoveController.playerState != PlayerState.Run)
        {
            playerMoveController.playerState = PlayerState.Walk;
        }
    
        Vector3 direction = playerMoveController.direction;
        (Vector3 scaledMovement, Quaternion targetRotation) = ScaleMovement(direction, playerMoveController.moveType);
        
        navMeshAgent.transform.rotation = Quaternion.Slerp
        (
            navMeshAgent.transform.rotation, 
            targetRotation, 
            Mathf.Clamp01(Time.deltaTime * playerMoveController.lookAtSpeed)
        );
        
        navMeshAgent.Move(scaledMovement);
    
        MoveAnimation();
    }

    private (Vector3, Quaternion) ScaleMovement(Vector3 direction, PlayerMoveController.MoveType moveType)
    {
        switch (moveType)
        {
            case PlayerMoveController.MoveType.Absolute:
                return AbsoluteDirection(direction);
            case PlayerMoveController.MoveType.Relative:
                return RelativeDirection(direction);
        }

        return (Vector3.zero, Quaternion.identity);
    }

    private (Vector3, Quaternion) AbsoluteDirection(Vector3 direction)
    {
        Vector3 absoluteDirection = new Vector3(direction.x, 0, direction.y).normalized;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime * absoluteDirection;
        
        Quaternion targetRotation = Quaternion.LookRotation(scaledMovement, Vector3.up);
        
        return (scaledMovement, targetRotation);
    }
    
    private (Vector3, Quaternion) RelativeDirection(Vector3 direction)
    {
        Vector3 relativeDirection 
            = navMeshAgent.transform.TransformDirection(new Vector3(direction.x, 0, direction.y)).normalized;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime * relativeDirection;
        
        Quaternion targetRotation = Quaternion.LookRotation(
            direction.y < 0 ? -scaledMovement : scaledMovement, Vector3.up);
        
        return (scaledMovement, targetRotation);
    }

    private void MoveAnimation()
    {
        playerAnimationController.RunStart();
    }
}
