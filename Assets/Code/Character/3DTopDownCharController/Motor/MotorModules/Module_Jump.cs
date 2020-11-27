using UnityEngine;
using UnityEngine.UIElements;

/*=== TERMINOLOGIES ===
 * COYOTE TIME: After walking off a platform, the player has a brief moment where they can still jump.
 * JUMP QUEUE TIMER: The player can cache the jump command for a brief moment while midair. If it is cached right before landing, the player will automatically jump after landing. 
 */


public class Module_Jump : ModuleBase
{
    private const float MaxJumpQueueDuration = 0.05f;

    public Module_Jump(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    #region Public 
    public override void ModuleEntry()
    {
        base.ModuleEntry();
        if (status.jumpQueueTimer > 0f)
        {
            OnJumpBtnDown();
        }
    }

    public override void TickUpdate()
    {
        TickTimers();

        if (GameInput.JumpBtnDown) // && !isJumping for onGround
        {
            if (status.canJump)
            {
                OnJumpBtnDown();
            }
            //else
            //{
            //    motorStatus.jumpQueueTimer = MaxJumpQueueDuration;
            //}
        }

        if (GameInput.JumpBtnUp)
        {
            OnJumpBtnUp();
        }
    }

    public override void TickFixedUpdate()
    {
        base.TickFixedUpdate();
        CheckIfJustWalkeOffPlatform();

        if (status.justLanded)
        {
            status.isJumping = false;
            status.coyoteTimer = -1f;
        }
    }

    //public override void TickFixedUpdate()
    //{
    //    base.TickFixedUpdate();
    //    CheckIfJustWalkeOffPlatform();

    //    if (motorStatus.justLanded)
    //    {
    //        //If player just landed and has a jump queued up.
    //        //if (motorStatus.jumpQueueTimer > 0f)
    //        //{
    //        //    OnJumpBtnDown();
    //        //}
    //        //else
    //        {
    //            motorStatus.isJumping = false;
    //            motorStatus.coyoteTimer = -1f;
    //        }
    //    }
    //}
    #endregion

    private void OnJumpBtnDown()
    {
        status.isJumping = true;
        status.jumpQueueTimer = -1f;
        status.coyoteTimer = -1f;

        status.currentVelocity.y = settings.MaxJumpForce;
    }

    private void OnJumpBtnUp()
    {
        if (status.currentVelocity.y > settings.MinJumpForce)
        {


            status.currentVelocity.y = settings.MinJumpForce;
        }
    }

    private void TickTimers()
    {
        if (status.coyoteTimer > 0f)
        {
            status.coyoteTimer -= Time.deltaTime;
        }

        if (status.jumpQueueTimer > 0f)
        {
            status.jumpQueueTimer -= Time.deltaTime;
        }
    }

    private void CheckIfJustWalkeOffPlatform()
    {
        if (!status.isOnGround && status.isOnGroundPrevious && !status.isJumping)
        {
            status.coyoteTimer = settings.MaxCoyoteDuration;
        }
    }
}

//public override void TickUpdate()
//{
//    if (status.currentVelocity.y > 0)
//    {
//        status.currentVelocity.y = 0;
//    }
//}
