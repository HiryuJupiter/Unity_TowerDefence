using UnityEngine;
using System.Collections.Generic;

public class MotorState_MoveOnGround : MotorStateBase
{
    public MotorState_MoveOnGround(PlayerTopDown3DController player, PlayerFeedbacks feedback) : base(player, feedback)
    {
        modules = new List<ModuleBase>()
        {
            new Module_Gravity(player, feedback),
            new Module_MoveOnGround(player, feedback),
            new Module_Jump(player, feedback),
            new Module_BasicAttack(player, feedback),
        };
    }

    public override void StateEntry()
    {
        base.StateEntry();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
    }

    protected override void Transitions()
    {
        //Go to aerial state if not on ground in current frame and in previous frame, or if moving up (jumping). We check for previous frame because when the player lands on a slope, they can come in and out of isOnGround status for 1 frame.
        if (!motorStatus.isOnGround && (motorStatus.currentVelocity.y > 0f || !motorStatus.isOnGroundPrevious))
        {
            player.SwitchToNewState(MotorStates.Aerial);
        }
    }
}