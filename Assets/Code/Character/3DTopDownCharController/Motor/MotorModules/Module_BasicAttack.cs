using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module_BasicAttack : ModuleBase
{
	const float AttackDuration = 0.5f;
	public Module_BasicAttack(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) {}

	public override void TickUpdate()
	{
		if (Input.GetMouseButtonDown(0) && !CursorManager.IsMouseOverUI)
		{
			feedback.Animator.PlayAttack();
			status.SetAttackAnimationTimer(player, AttackDuration);
		}
	}
}

/*
//Handle crouching input. If holding the crouch button but not crouching, crouch
		if (input.crouchHeld && !isCrouching && !isJumping)
			Crouch();
		//Otherwise, if not holding crouch but currently crouching, stand up
		else if (!input.crouchHeld && isCrouching)
			StandUp();
		//Otherwise, if crouching and no longer on the ground, stand up
		else if (!isOnGround && isCrouching)
			StandUp();

//If the player is crouching, reduce the velocity
		if (isCrouching)
			xVelocity /= crouchSpeedDivisor;

//If crouch is pressed...
			if (input.crouchPressed)
			{
				//...let go...
				isHanging = false;
				//...set the rigidbody to dynamic and exit
				rigidBody.bodyType = RigidbodyType2D.Dynamic;
				return;
			}

//...check to see if crouching AND not blocked. If so...
			if (isCrouching && !isHeadBlocked)
			{
				//...stand up and apply a crouching jump boost
				StandUp();
				rigidBody.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
			}


void Crouch()
	{
		//The player is crouching
		isCrouching = true;

		//Apply the crouching collider size and offset
		bodyCollider.size = colliderCrouchSize;
		bodyCollider.offset = colliderCrouchOffset;
	}


void StandUp()
	{
		//If the player's head is blocked, they can't stand so exit
		if (isHeadBlocked)
			return;

		//The player isn't crouching
		isCrouching = false;
	
		//Apply the standing collider size and offset
		bodyCollider.size = colliderStandSize;
		bodyCollider.offset = colliderStandOffset;
	}
 */