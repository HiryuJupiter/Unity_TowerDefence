using UnityEngine;
using System.Collections;

public abstract class ModuleBase
{
    protected CharacterSettings settings;
    protected PlayerTopDown3DController player;
    protected PlayerStatus status;
    protected PlayerFeedbacks feedback;
    protected MotorRaycaster raycaster;


    public ModuleBase(PlayerTopDown3DController player, PlayerFeedbacks feedback)
    {
        this.player = player;
        this.feedback = feedback;
        status = player.Status;
        raycaster   = player.Raycaster;

        settings = CharacterSettings.instance;
    }

    public virtual void ModuleEntry() { }
    public virtual void TickFixedUpdate() { }
    public virtual void TickUpdate() { }
    public virtual void ModuleExit() { }
}