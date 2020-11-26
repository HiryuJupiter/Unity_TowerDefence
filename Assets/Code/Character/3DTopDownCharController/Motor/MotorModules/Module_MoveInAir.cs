using UnityEngine;
using System.Collections;

public class Module_MoveInAir : ModuleBase
{
    public Module_MoveInAir(PlayerMotor motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    private float moveXSmoothDampVelocity;

    public override void TickUpdate()
    {
        base.TickUpdate();
        feedback.RotateCharacter();
    }

    public override void TickFixedUpdate()
    {
        //Move
        motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
        motorStatus.currentVelocity.z = Mathf.SmoothDamp(motorStatus.currentVelocity.z, GameInput.MoveZ * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
    }
}