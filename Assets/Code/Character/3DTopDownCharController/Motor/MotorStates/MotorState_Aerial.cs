using UnityEngine;
using System.Collections.Generic;


public class MotorState_Aerial : MotorStateBase
{
    public MotorState_Aerial(PlayerMotor motor, PlayerFeedbacks feedbacks) : base(motor, feedbacks)
    {
        modules = new List<ModuleBase>()
        {
            new Module_Gravity(motor, feedbacks),
            new Module_MoveInAir(motor, feedbacks),
            new Module_Jump(motor, feedbacks),
        };
    }

    public override void StateEntry()
    {
        base.StateEntry();
        feedback.Animator.PlayAerial();
    }

    public override void TickUpdate()
    {
        base.TickUpdate();
        feedback.Animator.SetFloat_YVelocity(motorStatus.currentVelocity.y);
    }

    protected override void Transitions()
    {
        if (motorStatus.isOnGround && (!motorStatus.isMovingUp || !motorStatus.isJumping))
        {
            motor.SwitchToNewState(MotorStates.OnGround);
        }
    }
}