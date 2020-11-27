using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MotorRaycaster))]
public class PlayerMotor : MonoBehaviour
{
    //Class and components
    private Rigidbody rb;
    private PlayerFeedbacks Feedbacks;
    private CharacterMotorSettings settings;

    //States
    private MotorStates currentStateType;
    private MotorStateBase currentStateClass;
    private Dictionary<MotorStates, MotorStateBase> stateClassLookup;

    public MotorStatus status { get; private set; }
    public MotorRaycaster raycaster { get; private set; }


    #region Public
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
    #endregion

    #region MonoBehiavor
    private void Awake()
    {
        //Reference
        rb = GetComponent<Rigidbody>();
        raycaster = GetComponent<MotorRaycaster>();
        Feedbacks = GetComponentInChildren<PlayerFeedbacks>();

        //Initialize
        status = new MotorStatus();
        stateClassLookup = new Dictionary<MotorStates, MotorStateBase>
        {
            {MotorStates.OnGround,  new MotorState_MoveOnGround(this, Feedbacks)},
            {MotorStates.Aerial,    new MotorState_Aerial(this, Feedbacks)},
            //{MotorStates.Hurt,      new MotorState_Hurt(this, Feedbacks)},
        };

        currentStateType = MotorStates.OnGround;
        currentStateClass = stateClassLookup[currentStateType];
    }

    private void Start()
    {
        settings = CharacterMotorSettings.instance;
    }

    private void Update()
    {
        currentStateClass?.TickUpdate();
    }

    private void FixedUpdate()
    {
        status.CachePreviousStatus();
        CalculateCurrentStatus();

        currentStateClass?.TickFixedUpdate();
        rb.velocity = transform.TransformDirection(status.currentVelocity);
    }
    #endregion

    #region Public 
    public void DamagePlayer(Vector2 enemyPos)
    {
        //status.lastEnemyPosition = enemyPos;
        //SwitchToNewState(MotorStates.Hurt);
    }

    public void RotateCharacter (float amount)
    {
        Quaternion q = Quaternion.AngleAxis(amount, Vector3.up);
        var targetRot = rb.rotation * q;
        rb.rotation = targetRot;
    }
    #endregion

    #region Pre-calculations
    private void CalculateCurrentStatus()
    {
        status.isOnGround = raycaster.OnGrounDcheck();
    }
    #endregion

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 20), "Current State: " + currentStateType); 

        GUI.Label(new Rect(20, 60, 290, 20), "=== GROUND MOVE === ");
        GUI.Label(new Rect(20, 80, 290, 20), "OnGround: " + status.isOnGround);
        GUI.Label(new Rect(20, 100, 290, 20), "onGroundPrevious: " + status.isOnGroundPrevious);
        GUI.Label(new Rect(20, 120, 290, 20), "GameInput.MoveX: " + GameInput.MoveX);
        GUI.Label(new Rect(20, 180, 290, 20), "currentVelocity: " + status.currentVelocity);


        GUI.Label(new Rect(200, 0, 290, 20), "=== JUMPING === ");
        GUI.Label(new Rect(200, 20, 290, 20), "coyoteTimer: " + status.coyoteTimer);
        GUI.Label(new Rect(200, 40, 290, 20), "jumpQueueTimer: " + status.jumpQueueTimer);
        GUI.Label(new Rect(200, 60, 290, 20), "GameInput.JumpBtnDown: " + GameInput.JumpBtnDown);
        GUI.Label(new Rect(200, 80, 290, 20), "jumping: " + status.isJumping);

        GUI.Label(new Rect(400, 0, 290, 20), "=== INPUT === ");
        GUI.Label(new Rect(400, 20, 290, 20), "MoveX: " + GameInput.MoveX);
        GUI.Label(new Rect(400, 40, 290, 20), "MoveZ: " + GameInput.MoveZ);

        //GUI.Label(new Rect(300, 120,		290, 20), "testLocation: " + testLocation);
    }
}
