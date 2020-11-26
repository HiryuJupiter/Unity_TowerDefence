using UnityEngine;
using System.Collections;
using UnityEditor;

public class Module_Gravity : ModuleBase
{
    public Module_Gravity(PlayerMotor motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

    public override void TickFixedUpdate()
    {
        //Apply gravity when not on ground
        if (motorStatus.isOnGround)
        {
            if (motorStatus.isFalling)
            {
                motorStatus.currentVelocity.y = 0;
            }
        }
        else
        {

            Debug.Log(" Applying gravity ");
            motorStatus.currentVelocity.y -= settings.Gravity * Time.deltaTime;
            motorStatus.currentVelocity.y = Mathf.Clamp(motorStatus.currentVelocity.y, settings.MaxFallSpeed, motorStatus.currentVelocity.y);
        }
    }
}
