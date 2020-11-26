using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerFeedbacks))]

public class PlayerTopDown3DController : MonoBehaviour
{
    //Components and classes
    private PlayerMotor motor;
    private PlayerFeedbacks feedbacks;

	#region MonoBehavior
	public void Awake()
	{
		motor = GetComponent<PlayerMotor>();
		feedbacks = GetComponentInChildren<PlayerFeedbacks>();
	}

	public void Update()
    {
		//motor.OnUpdate();
		//graphics.OnUpdate();

		if (Input.GetKeyDown(KeyCode.H))
			DamagePlayer(Vector2.zero, 1);
	}
	public void FixedUpdate()
	{
		//motor.OnFixedUpdate();
	}
	#endregion

	public void DamagePlayer(Vector2 enemyPos, int damage)
	{
		motor.DamagePlayer(enemyPos);
	}
}