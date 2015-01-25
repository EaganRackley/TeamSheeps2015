using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Enum for directions the player is facing.
    // 
    //       N
    //    NW | NE
    //  W -- * -- E
    //    SW | SE
    //       S
    // 
    public enum FacingDirection { N, NE, E, SE, S, SW, W, NW };
    public delegate IEnumerator PlayerPowerup(PlayerController player);
    // Movement amount (for all movement directions).
    public float speed;
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;


    // Direction keycodes; to be set in the scene editor.
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    private Rigidbody body;
    [HideInInspector]
    public Light playerLight;
    protected Animator m_Animator;

    private Vector3 ROTATION_NORTH = new Vector3(0.0f, 0.0f, 180.0f);
    private Vector3 ROTATION_EAST = new Vector3(0.0f, 0.0f, 90.0f);
    private Vector3 ROTATION_SOUTH = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 ROTATION_WEST = new Vector3(0.0f, 0.0f, 270.0f);

	private Vector3 ROTATION_NORTHEAST = new Vector3(0.0f, 0.0f, 135.0f);
	private Vector3 ROTATION_SOUTHEAST = new Vector3(0.0f, 0.0f, 45.0f);
	private Vector3 ROTATION_SOUTHWEST = new Vector3(0.0f, 0.0f, 315.0f);
	private Vector3 ROTATION_NORTHWEST = new Vector3(0.0f, 0.0f, 225.0f);

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Called on load.
    void Awake()
    {
        this.body = GetComponent<Rigidbody>();
        this.playerLight = transform.GetComponentInChildren<Light>();
    }

    // Called once per timestep.
    void FixedUpdate()
    {

    }

	// Examines the current facing direction and sets rotation accordingly.
	public void HandlePlayerRotation()
	{
		if (this.currentDirection == FacingDirection.N)
		{
			this.transform.eulerAngles = (ROTATION_NORTH);
		}
		else if (this.currentDirection == FacingDirection.NE)
		{
			this.transform.eulerAngles = (ROTATION_NORTHEAST);
		}
		else if (this.currentDirection == FacingDirection.E)
		{
			this.transform.eulerAngles = (ROTATION_EAST);
		}
		else if (this.currentDirection == FacingDirection.SE)
		{
			this.transform.eulerAngles = ROTATION_SOUTHEAST;
		}
		else if (this.currentDirection == FacingDirection.S)
		{
			this.transform.eulerAngles = (ROTATION_SOUTH);
		}
		else if (this.currentDirection == FacingDirection.SW)
		{
			this.transform.eulerAngles = ROTATION_SOUTHWEST;
		}
		else if (this.currentDirection == FacingDirection.W)
        {
            this.transform.eulerAngles = (ROTATION_WEST);
        }
        else if (this.currentDirection == FacingDirection.NW)
        {
            this.transform.eulerAngles = ROTATION_NORTHWEST;
		}
	}

    // Called once each frame.
    void Update()
    {


        // If no buttons are pressed, velocity is 0.
        float velocity_x = 0f;
        float velocity_y = 0f;

        // Set x component of velocity.
        if (Input.GetKey(upKey))
        {
            velocity_y += speed;
            this.currentDirection = FacingDirection.N;
        }
        else if (Input.GetKey(downKey))
        {
            velocity_y -= speed;
            this.currentDirection = FacingDirection.S;
        }

        // Set x component of velocity.
        if (Input.GetKey(leftKey))
        {
            velocity_x -= speed;
            this.currentDirection = FacingDirection.W;
        }
        else if (Input.GetKey(rightKey))
        {
            velocity_x += speed;
            this.currentDirection = FacingDirection.E;
        }

		// Change the facing direction according to current velocity direction.
		if (velocity_x > 0) {
			if (velocity_y > 0) this.currentDirection = FacingDirection.NE;
			if (velocity_y == 0) this.currentDirection = FacingDirection.E;
			if (velocity_y < 0) this.currentDirection = FacingDirection.SE;
		}
		else if (velocity_x == 0) {
			if (velocity_y > 0) this.currentDirection = FacingDirection.N;
			if (velocity_y == 0) {/*do nothing*/}
			if (velocity_y < 0) this.currentDirection = FacingDirection.S;
		}
		else if (velocity_x < 0) {
			if (velocity_y > 0) this.currentDirection = FacingDirection.NW;
			if (velocity_y == 0) this.currentDirection = FacingDirection.W;
            if (velocity_y < 0) this.currentDirection = FacingDirection.SW;
		}

        // Set the new velocity.
		float down_velocity = this.body.velocity.z;
        this.body.velocity = new Vector3(velocity_x, velocity_y, 0f).normalized * this.speed;
		this.body.velocity += new Vector3(0f, 0f, down_velocity);

        // Apply the new velocity to our animations
        if (this.body.velocity.x != 0 || this.body.velocity.y != 0)
        {
            m_Animator.SetBool("IsMoving", true);
        }
        else
        {
            m_Animator.SetBool("IsMoving", false);
        }


        // Apply our current direction to the player rotation
        HandlePlayerRotation();
    }

    public void GetPowerup(PlayerPowerup powerupFunction)
    {
        StartCoroutine(powerupFunction(this));
    }
}
