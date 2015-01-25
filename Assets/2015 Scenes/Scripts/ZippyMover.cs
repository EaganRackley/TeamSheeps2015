using UnityEngine;
using System.Collections;

public class ZippyMover : MonoBehaviour {

	public float moveSpeed;
	
	public float timePerTurningDirection;
	public float randomTimePerTurningDirection;
	public float turnSpeed;
	public float randomTurnSpeed;
	bool turningRight = false;
	
	float turningTimer;
	
	const float UPDOWN_RANGE = 0.5f;
	const float SECONDS_PER_CYCLE = 5f;
	float updownTimer;
	float initialHeight;
	
	float currentDirection = 0f;

	void Awake () {
		initialHeight = transform.position.z;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
		MoveAround();
	}

	void MoveUpDown()
	{
		updownTimer += Time.deltaTime * (1f / SECONDS_PER_CYCLE);
		if (updownTimer > 1f)
			updownTimer -= 1f;
		transform.position = new Vector3(transform.position.x, transform.position.y, initialHeight + Mathf.Sin(updownTimer * 2 * Mathf.PI) * UPDOWN_RANGE);
	}

	void MoveAround()
	{
		this.turningTimer -= Time.deltaTime;
		if (turningTimer <= 0f)
		{
			this.turningTimer = timePerTurningDirection + Random.Range(0f, randomTimePerTurningDirection);
			this.turningRight = !this.turningRight;
		}
		if (this.turningRight)
		{
			currentDirection -= turnSpeed + Random.Range(0f, randomTurnSpeed);
		}
		else
		{
			currentDirection += turnSpeed + Random.Range(0f, randomTurnSpeed);
		}
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentDirection);
		Vector2 movementVector = new Vector2(Mathf.Cos(currentDirection * Mathf.Deg2Rad), Mathf.Sin(currentDirection * Mathf.Deg2Rad));
		transform.position += new Vector3(movementVector.x, movementVector.y, 0f) * moveSpeed * Time.deltaTime;
	}
}
