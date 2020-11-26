using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-9000)] 
public class CharacterMotorSettings : MonoBehaviour
{
    public static CharacterMotorSettings instance { get; private set; }

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;


    [Header("Player Movement")]
    [SerializeField] private float steerSpeedGround = 1f; //50f
    [SerializeField] private float steerSpeedAir = 5f; //50f
    [SerializeField] private float playeRunSpeed = 15;
    [SerializeField] private float playerWalkSpeed = 10;
    [SerializeField] private float playerCrouchSpeed = 6;
    public float SteerSpeedGround => steerSpeedGround;
    public float SteerSpeedAir => steerSpeedAir;
    public float PlayerRunSpeed => playeRunSpeed;
    public float PlayerMoveSpeed => playerWalkSpeed;
    public float PlayerCrawlSpeed => playerCrouchSpeed;


    [Header("Normal Jump")]
    [SerializeField] private float minJumpForce = 12f;
    [SerializeField] private float maxJumpForce = 22f;
    [SerializeField] private float maxCoyoteDuration = 0.25f;
    public float MinJumpForce => minJumpForce;
    public float MaxJumpForce => maxJumpForce;
    public float MaxCoyoteDuration => maxCoyoteDuration;

    [Header("Hurt State")]
    [Range(10f, 50f)] [SerializeField] private float hurtSlideSpeed = 20f; //50f

    [SerializeField] private Vector3 hurtDirection = new Vector3(0f, 25f, 20f);
    [SerializeField] private float hurtDuration = 0.5f;
    public float HurtSlideSpeed => hurtSlideSpeed;
    public Vector2 HurtDirection => hurtDirection;
    public float HurtDuration => hurtDuration;

    [Header("Gravity")]
    
    [SerializeField] private float maxFallSpeed = -15f;
    [SerializeField] private float gravity = 80f;
    public float MaxFallSpeed => maxFallSpeed;
    public float Gravity => gravity;

    [Header("Crouch")]
    [SerializeField] private Vector2 crouchOffset;
    [SerializeField] private Vector2 crouchSize;
    public Vector2 CrouchOffset => crouchOffset;
    public Vector2 CrouchSize => crouchSize;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
        }
    }
}