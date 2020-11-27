using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MotorRaycaster))]
public class PlayerTopDown3DController : MonoBehaviour
{
    //Class and components
    private PlayerFeedbacks feedback;
    private Player3rdPersonCamera cameraController;

    //States
    private MotorStates currentStateType;
    private MotorStateBase currentStateClass;
    private Dictionary<MotorStates, MotorStateBase> stateClassLookup;

    public PlayerStatus Status { get; private set; }
    public MotorRaycaster Raycaster { get; private set; }
    public Rigidbody Rb { get; private set; }

    #region MonoBehiavor
    private void Awake()
    {
        //Reference
        Rb = GetComponent<Rigidbody>();
        Raycaster = GetComponent<MotorRaycaster>();
        feedback = GetComponentInChildren<PlayerFeedbacks>();
        cameraController = GetComponent<Player3rdPersonCamera>();

        //Initialize
        Status = new PlayerStatus();
        stateClassLookup = new Dictionary<MotorStates, MotorStateBase>
        {
            {MotorStates.OnGround,  new MotorState_MoveOnGround(this, feedback)},
            {MotorStates.Aerial,    new MotorState_Aerial(this, feedback)},
            //{MotorStates.Hurt,      new MotorState_Hurt(this, Feedbacks)},
        };

        currentStateType = MotorStates.OnGround;
        currentStateClass = stateClassLookup[currentStateType];
    }

    private void Update()
    {
        currentStateClass?.TickUpdate();
    }


    private void FixedUpdate()
    {
        Status.CachePreviousStatus();
        CalculateCurrentStatus();

        currentStateClass?.TickFixedUpdate();

        ExecuteRigidbodyVelocity();
    }
    #endregion

    #region Public 
    public void DamagePlayer(Vector2 enemyPos, int damage)
    {
        //status.lastEnemyPosition = enemyPos;
        //SwitchToNewState(MotorStates.Hurt);
    }

    public void RotateCharacter (float amount)
    {
        //Rotate the character by multiplying rotation amount to current quaternion
        Quaternion q = Quaternion.AngleAxis(amount, Vector3.up);
        var targetRot = Rb.rotation * q;
        Rb.rotation = targetRot;
    }

    public void SwitchToNewState(MotorStates newStateType)
    {
        if (currentStateType != newStateType)
        {
            currentStateType = newStateType;

            currentStateClass.StateExit();
            currentStateClass = stateClassLookup[newStateType];
            currentStateClass.StateEntry();
        }
    }

    public void SetFacingBasedOnMovement ()
    {
        //Debug.DrawRay(transform.position, Rb.velocity, Color.red);
        Vector3 vel = Status.currentVelocity;
        vel.y = 0f;
        feedback.SetFacing(Quaternion.LookRotation(cameraController.NonTiltedRotationTowardsPlayer * vel, Vector3.up));
        //feedback.SetFacing(cameraController.NonTiltedRotationTowardsPlayer);
        //feedback.SetFacing(Quaternion.LookRotation(Rb.velocity, Vector3.up));
    }

    public void SetFacingToFront ()
    {
        //feedback.SetFacing(Quaternion.LookRotation(Vector3.left, Vector3.up));
        feedback.SetFacing(Quaternion.LookRotation(cameraController.NonTiltedDirectionTowardsPlayer, Vector3.up));

        //feedback.SetFacing(cameraController.NonTiltedRotationTowardsPlayer);
    }
    #endregion

    #region Private
    private void ExecuteRigidbodyVelocity ()
    {
        //rb.velocity = transform.TransformDirection(status.currentVelocity);
        //rb.velocity = camera.TransformDirection(status.currentVelocity);
        Rb.velocity = cameraController.NonTiltedRotationTowardsPlayer * Status.currentVelocity;
    }
    #endregion

    #region Pre-calculations
    private void CalculateCurrentStatus()
    {
        Status.isOnGround = Raycaster.OnGrounDcheck();
    }
    #endregion

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 500, 20), "Current State: " + currentStateType); 

    //    GUI.Label(new Rect(20, 60, 290, 20), "=== GROUND MOVE === ");
    //    GUI.Label(new Rect(20, 80, 290, 20), "OnGround: " + status.isOnGround);
    //    GUI.Label(new Rect(20, 100, 290, 20), "onGroundPrevious: " + status.isOnGroundPrevious);
    //    GUI.Label(new Rect(20, 120, 290, 20), "GameInput.MoveX: " + GameInput.MoveX);
    //    GUI.Label(new Rect(20, 180, 290, 20), "currentVelocity: " + status.currentVelocity);


    //    GUI.Label(new Rect(200, 0, 290, 20), "=== JUMPING === ");
    //    GUI.Label(new Rect(200, 20, 290, 20), "coyoteTimer: " + status.coyoteTimer);
    //    GUI.Label(new Rect(200, 40, 290, 20), "jumpQueueTimer: " + status.jumpQueueTimer);
    //    GUI.Label(new Rect(200, 60, 290, 20), "GameInput.JumpBtnDown: " + GameInput.JumpBtnDown);
    //    GUI.Label(new Rect(200, 80, 290, 20), "jumping: " + status.isJumping);

    //    GUI.Label(new Rect(400, 0, 290, 20), "=== INPUT === ");
    //    GUI.Label(new Rect(400, 20, 290, 20), "MoveX: " + GameInput.MoveX);
    //    GUI.Label(new Rect(400, 40, 290, 20), "MoveZ: " + GameInput.MoveZ);

    //    //GUI.Label(new Rect(300, 120,		290, 20), "testLocation: " + testLocation);
    //}
}
