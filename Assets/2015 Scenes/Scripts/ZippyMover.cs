using UnityEngine;
using System.Collections;

public class ZippyMover : MonoBehaviour {

	public float moveSpeed;
    public bool LimitX = false;
	public float timePerTurningDirection;
	public float randomTimePerTurningDirection;
	public float turnSpeed;
	public float randomTurnSpeed;
	bool turningRight = false;
	
	float turningTimer;
	
	public float UpDownRange = 0.5f;
	const float SECONDS_PER_CYCLE = 5f;
	float updownTimer;
	float initialHeight;
	
	float currentDirection = 0f;

	void Awake () {
		initialHeight = transform.position.z;
	}

	// Use this for initialization
	void Start () {
		currentDirection = Random.Range(0.0f, 360.0f);
	}
	
	// Update is called once per frame
	void Update () {
		MoveUpDown();
        MoveAround();
        if (LimitX)
        {
            if (this.transform.position.x < -0.0f) //-3
            {
                currentDirection = 360f;
            }
            else if (this.transform.position.x > 4f) //8
            {
                currentDirection = 180;
            }

            if (this.transform.position.y > 60.0f)
            {
                currentDirection = 270f;
            }
            else if (this.transform.position.y < 32f)
            {
                currentDirection = 90f;
            }
        }
    }

	void MoveUpDown()
	{
		updownTimer += Time.deltaTime * (1f / SECONDS_PER_CYCLE);
		if (updownTimer > 1f)
			updownTimer -= 1f;
		transform.position = new Vector3(transform.position.x, transform.position.y, initialHeight + Mathf.Sin(updownTimer * 2 * Mathf.PI) * UpDownRange);
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
