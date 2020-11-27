using UnityEngine;
using System.Collections;

public class Module_MoveInAir : ModuleBase
{
    const float RotationSpeed = 1f;
    public Module_MoveInAir(PlayerMotor motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    private float moveXSmoothDampVelocity;
    private float moveZSmoothDampVelocity;

    public override void TickUpdate()
    {
        base.TickUpdate();
        feedback.RotateCharacter();
    }

    public override void TickFixedUpdate()
    {
        //Move
        //motorStatus.currentVelocity.x = Mathf.SmoothDamp(motorStatus.currentVelocity.x, GameInput.MoveX * settings.PlayerMoveSpeed, ref moveXSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
        motorStatus.currentVelocity.z = Mathf.SmoothDamp(motorStatus.currentVelocity.z, GameInput.MoveZ * settings.PlayerMoveSpeed, ref moveZSmoothDampVelocity, settings.SteerSpeedAir * Time.deltaTime);
    }

    private void RotateCharacterWithMouse()
    {
        float mouseX = GameInput.MoveX;

        motor.RotateCharacter(GameInput.MoveX * RotationSpeed);
        //motor.RotateCharacter1(Input.GetAxis("Mouse X"));
    }
}