using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(Player3DAnimator))]
public class PlayerFeedbacks : MonoBehaviour
{
    [SerializeField] Transform modelTransform;

    //Component reference
    public Player3DAnimator Animator { get; private set; }

    //Facing
    private bool facingRight;
    private Vector3 faceRightScale, faceLeftScale;

    private void Awake()
    {
        Animator = GetComponent<Player3DAnimator>();

        //Cache the scales 
        faceRightScale = transform.localScale;
        faceLeftScale = faceRightScale;
        faceLeftScale.x *= -1f;
    }

    //public void RotateCharacter ()
    //{
    //    if (GameInput.PressedRight)
    //    {
    //        SetFacingToRight(true);
    //    }
    //    else if (GameInput.PressedLeft)
    //    {
    //        SetFacingToRight(false);
    //    }
    //}

    public void SetFacing(Quaternion facing)
    {
        modelTransform.localRotation = facing;
    }
}