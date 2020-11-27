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
    public Module_MoveOnGround(PlayerMotor motor, PlayerFeedbacks feedback) : base(motor, feedback)
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

        CharacterRotationUpdate();
        feedback.RotateCharacter();
        AnimationUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Modify x-velocity
        //motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * moveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
        motorStatus.currentVelocity.z = Mathf.SmoothDamp(motorStatus.currentVelocity.z, GameInput.MoveZ * moveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
        crawling = false;
    }
    #endregion

    private void CharacterRotationUpdate ()
    {
        float mouseX = GameInput.MoveX;

        motor.RotateCharacter(GameInput.MoveX * RotationSpeed);
        //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    }

    private void AnimationUpdate ()
    {
        if (!motorStatus.isInAttackAnimation)
        {
            if (GameInput.MoveX == 0 && GameInput.MoveZ == 0)
            {
                feedback.Animator.PlayIdle();
            }
            else
            {
                feedback.Animator.PlayWalk();
            }
        }
    }

}