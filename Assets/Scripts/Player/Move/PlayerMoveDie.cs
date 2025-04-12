using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDie : IMove
{
    private readonly PlayerMoveController playerMoveController;

    public PlayerMoveDie(PlayerMoveController playerMoveController)
    {
        this.playerMoveController = playerMoveController;
    }
    
    public void Move()
    {
        playerMoveController.playerState = PlayerState.Die;
    }
}
