using UnityEngine;
using System.Collections;
/*
 mechanim is not appliable to frame based animation, because you can just tell it to 
Instead of setfloat, just animator.Play(

Using setFloat you're offloading logic to another system that handles the transition, 
but you're probably also handling logic handling in code, it's much easier to do everything in code in one place

coolhotkey: alt + arrow to shift lines


 */
public class Player3DAnimator : MonoBehaviour
{

    //Component reference
    private Animator animator;
    private int currentState;

    //Parameter ID for states
    private int jumpParamID;
    private int idleParamID;
    private int walkParamID;
    private int attackParamID;

    bool inAttack;

    #region Mono
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        //Param ID: Booleans
        jumpParamID = Animator.StringToHash("Jump");
        idleParamID = Animator.StringToHash("Idle");
        walkParamID = Animator.StringToHash("Walk");
        attackParamID = Animator.StringToHash("Attack");
    }
    #endregion

    public void PlayIdle()
    {
        if (!inAttack)
        {
            ChangeAnimationState(idleParamID);
        }
    }

 

    public void PlayJump()
    {
        //ChangeAnimationState(jumpParamID);
    }
    public void PlayAttack()
    {
        ChangeAnimationState(attackParamID);
    }

    public void PlayWalk()
    {
        ChangeAnimationState(walkParamID);
    }

    private void ChangeAnimationState(int newState)
    {
        //Change to new animation state only if it's a different one
        if (currentState != newState)
        {
            animator.Play(newState);
            currentState = newState;
        }
    }

    private float GetCurrentAnimationDuration()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private IEnumerator DelayedTransitionToAnimation(float delay, int newAnimationParamID)
    {
        yield return new WaitForSeconds(delay);
        animator.Play(newAnimationParamID);
    }
}