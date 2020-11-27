using UnityEngine;
using System.Collections;

public class Module_MoveInAir : ModuleBase
{
    const float RotationSpeed = 1f;
    public Module_MoveInAir(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    private float moveXSmoothDampVelocity;
    private float moveZSmoothDampVelocity;

    public override void TickUpdate()
    {
        AnimationUpdate();
        base.TickUpdate();
    }

    public override void TickFixedUpdate()
    {
        //Move
        //motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
        status.currentVelocity.z = Mathf.SmoothDamp(status.currentVelocity.z, GameInput.MoveZ * settings.PlayerMoveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
    }

    //private void RotateCharacterWithMouse()
    //{
    //    float mouseX = GameInput.MoveX;

    //    player.RotateCharacter(GameInput.MoveX * RotationSpeed);
    //    //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    //}

    private void AnimationUpdate()
    {
        if (GameInput.IsMoving)
        {
            player.SetFacingBasedOnMovement();
        }
    }
}