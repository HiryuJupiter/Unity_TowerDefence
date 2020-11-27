using UnityEngine;
using System.Collections;

public class MotorStatus
{
    //Attck
    public bool isInAttackAnimation;

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
    public bool canJump => isOnGround || !isInAttackAnimation || (coyoteTimer > 0f && !isJumping);
    public bool justLanded => !isOnGroundPrevious && isOnGround;

    public void CachePreviousStatus()
    {
        isOnGroundPrevious = isOnGround;
    }

    public void SetAttackAnimationTimer (MonoBehaviour mono, float duration)
    {
        mono.StartCoroutine(TickAttackAnimationTimer(duration));
    }

    private IEnumerator TickAttackAnimationTimer (float duration)
    {
        isInAttackAnimation = true;
        yield return new WaitForSeconds(duration);
        isInAttackAnimation = false;
    }

}