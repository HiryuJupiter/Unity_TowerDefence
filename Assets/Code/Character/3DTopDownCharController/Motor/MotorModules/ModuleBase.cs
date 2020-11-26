using UnityEngine;
using System.Collections;

public abstract class ModuleBase
{
    protected CharacterMotorSettings settings;
    protected PlayerMotor motor;
    protected MotorStatus motorStatus;
    protected PlayerFeedbacks feedback;
    protected MotorRaycaster raycaster;


    public ModuleBase(PlayerMotor motor, PlayerFeedbacks feedback)
    {
        this.motor = motor;
        this.feedback = feedback;
        motorStatus = motor.status;
        raycaster   = motor.raycaster;

        settings = CharacterMotorSettings.instance;
    }

    public virtual void ModuleEntry() { }
    public virtual void TickFixedUpdate() { }
    public virtual void TickUpdate() { }
    public virtual void ModuleExit() { }
}