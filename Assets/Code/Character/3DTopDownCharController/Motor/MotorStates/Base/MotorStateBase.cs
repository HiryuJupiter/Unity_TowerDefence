using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MotorRaycaster))]
[RequireComponent(typeof(Module_Jump))]
public abstract class MotorStateBase
{
    protected CharacterMotorSettings      settings;
    protected PlayerMotor     motor;
    protected PlayerFeedbacks feedback;
    protected MotorStatus       motorStatus;
    protected MotorRaycaster    raycaster;

    protected List<ModuleBase> modules = new List<ModuleBase>();

    public MotorStateBase(PlayerMotor motor, PlayerFeedbacks feedback)
    {
        this.motor      = motor;
        this.feedback   = feedback;
        motorStatus     = motor.status;
        raycaster       = motor.raycaster;
        settings        = CharacterMotorSettings.instance;
    }

    public virtual void StateEntry()
    {
        foreach (var m in modules)
        {
            m.ModuleEntry();
        }
    }

    public virtual void TickUpdate() 
    {
        foreach (var m in modules)
        {
            m.TickUpdate();
        }
    }

    public virtual void TickFixedUpdate()
    {
        foreach (var m in modules)
        {
            m.TickFixedUpdate();
        }
        Transitions();
    }

    public virtual void StateExit()
    {
        foreach (var m in modules)
        {
            m.ModuleExit();
        }
    }

    protected abstract void Transitions();
}