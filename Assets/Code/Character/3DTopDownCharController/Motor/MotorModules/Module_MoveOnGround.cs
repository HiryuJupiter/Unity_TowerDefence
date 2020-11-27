using UnityEngine;
using System.Collections;

public class Module_MoveOnGround : ModuleBase
{
    const float RotationSpeed = 2f;

    private float moveXSmoothDampVelocity;
    private float moveZSmoothDampVelocity;
    private bool crawling;

    private float moveSpeed => crawling ? settings.PlayerCrawlSpeed : settings.PlayerMoveSpeed;

    //Ctor
    public Module_MoveOnGround(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    #region Public methods
    public override void ModuleEntry()
    {
        base.ModuleEntry();
        AnimationUpdate();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();

        //CharacterRotationUpdate();
        AnimationUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Modify x-velocity
        status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, GameInput.MoveX * moveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
        status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, GameInput.MoveZ * moveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
        crawling = false;
    }
    #endregion

    //private void CharacterRotationUpdate ()
    //{
    //    float mouseX = GameInput.MoveX;

    //    player.RotateCharacter(GameInput.MoveX * RotationSpeed);
    //    //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    //}

    private void AnimationUpdate ()
    {
        if (status.isInAttackAnimation)
        {
            player.SetFacingToFront();
        }
        else
        {
            if (GameInput.IsMoving)
            {
                feedback.Animator.PlayWalk();
                //player.SetFacingToFront();
                //player.SetFacingBasedOnMovement();
                player.SetFacingBasedOnMovement();
            }
            else
            {
                feedback.Animator.PlayIdle();
            }
        }
    }

}