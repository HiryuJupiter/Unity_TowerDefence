using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MotorRaycaster))]
[RequireComponent(typeof(Module_Jump))]
public abstract class MotorStateBase
{
    protected CharacterSettings      settings;
    protected PlayerTopDown3DController     player;
    protected PlayerFeedbacks feedback;
    protected PlayerStatus       motorStatus;
    protected MotorRaycaster    raycaster;

    protected List<ModuleBase> modules = new List<ModuleBase>();

    public MotorStateBase(PlayerTopDown3DController player, PlayerFeedbacks feedback)
    {
        this.player      = player;
        this.feedback   = feedback;
        motorStatus     = player.Status;
        raycaster       = player.Raycaster;
        settings        = CharacterSettings.instance;
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