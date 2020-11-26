using UnityEngine;
using System.Collections;

public class Module_MoveOnGround : ModuleBase
{
    public Module_MoveOnGround(PlayerMotor motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    private float moveXSmoothDampVelocity;

    private float moveSpeed => crawling ? settings.PlayerCrawlSpeed : settings.PlayerMoveSpeed;

    private bool crawling;

    public override void ModuleEntry()
    {
        base.ModuleEntry();
        Stand();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();

        feedback.RotateCharacter();

        StanceUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Modify x-velocity
        motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * moveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
        motorStatus.currentVelocity.z = Mathf.SmoothDamp(motorStatus.currentVelocity.z, GameInput.MoveZ * moveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedGround * Time.deltaTime);
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
        crawling = false;
    }

    private void StanceUpdate ()
    {
        if (!crawling && GameInput.PressedDown)
        {
            Crawl();
        }
        else if (crawling && !GameInput.PressedDown)
        {
            Stand();
        }
    }

    private void Stand ()
    {
        crawling = false;
        feedback.Animator.PlayOnGround();
    }

    private void Crawl ()
    {
        crawling = true;
        feedback.Animator.PlayCrouch();
    }
}