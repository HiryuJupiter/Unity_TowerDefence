﻿using UnityEngine;

public class MotorStatus
{
    //Move
    public bool isOnGround;
    public bool isOnGroundPrevious;
    public bool isJumping;
    public Vector3 currentVelocity;

    //Jump
    public float coyoteTimer;
    public float jumpQueueTimer;

    //Hurt state
    public Vector2 lastEnemyPosition;

    //Convenience properties
    public bool isFalling => currentVelocity.y < 0f;
    public bool isMovingUp => currentVelocity.y > 0f;
    //public bool canJump => isOnGround || (coyoteTimer > 0f && !isJumping);
    public bool canJump => isOnGround || (coyoteTimer > 0f && !isJumping);
    public bool justLanded => !isOnGroundPrevious && isOnGround;

    public void CachePreviousStatus()
    {
        isOnGroundPrevious = isOnGround;
    }
}