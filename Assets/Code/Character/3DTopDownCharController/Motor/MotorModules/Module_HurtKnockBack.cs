using UnityEngine;
using System.Collections;

public class Module_HurtKnockBack : ModuleBase
{
    public Module_HurtKnockBack(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
    { }

    private float moveXSmoothDampVelocity;

    public override void ModuleEntry()
    {
        base.ModuleEntry();

        //motorStatus.currentVelocity.x = motorStatus.
        //motorStatus.currentVelocity = settings.HurtForce;
    }

    public override void TickFixedUpdate()
    {
        //motorStatus.currentVelocity = Vector3.SmoothDamp(motorStatus.currentVelocity, 0f, ref moveXSmoothDampVelocity, settings.HurtSlideSpeed * Time.deltaTime);
    }

    public override void ModuleExit()
    {
        base.ModuleExit();
    }
}
