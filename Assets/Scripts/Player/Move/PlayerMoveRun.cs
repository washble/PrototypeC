using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveRun : IMove
{
    private readonly PlayerMoveController playerMoveController;
    private readonly PlayerAnimationController playerAnimationController;
    private readonly NavMeshAgent navMeshAgent;

    private bool isSpeedRunning = false;

    public PlayerMoveRun(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
        playerAnimationController = PlayerAnimationController.Instance;
        
        navMeshAgent = playerMoveController.navMeshAgent;
    }
    
    public void Move()
    {
        playerMoveController.playerState = PlayerState.Run;
    
        Vector3 direction = playerMoveController.direction;
        Vector3 scaledMovement = navMeshAgent.speed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
        
        Quaternion targetRotation = Quaternion.LookRotation(scaledMovement, Vector3.up);
        navMeshAgent.transform.rotation = Quaternion.Slerp
                                        (
                                            navMeshAgent.transform.rotation, 
                                            targetRotation, 
                                            Mathf.Clamp01(Time.deltaTime * playerMoveController.lookAtSpeed)
                                        );
        
        navMeshAgent.Move(scaledMovement);
    
        MoveAnimation();
    }

    public void StartSpeedRunning()
    {
        if(isSpeedRunning) { return; }

        isSpeedRunning = true;
        navMeshAgent.speed += playerMoveController.playerWeaponController.speed;
    }

    public void StopSpeedRunning()
    {
        if(!isSpeedRunning) { return; }

        isSpeedRunning = false;
        navMeshAgent.speed -= playerMoveController.playerWeaponController.speed;
    }

    private void MoveAnimation()
    {
        playerAnimationController.RunStart();
    }
}
