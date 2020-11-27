using UnityEngine;
using System.Collections;
using UnityEditor;

public class Module_Gravity : ModuleBase
{
    public Module_Gravity(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

    public override void TickFixedUpdate()
    {
        //Apply gravity when not on ground
        if (status.isOnGround)
        {
            if (status.isFalling)
            {
                status.currentVelocity.y = 0;
            }
        }
        else
        {

            status.currentVelocity.y -= settings.Gravity * Time.deltaTime;
            status.currentVelocity.y = Mathf.Clamp(status.currentVelocity.y, settings.MaxFallSpeed, status.currentVelocity.y);
        }
    }
}
