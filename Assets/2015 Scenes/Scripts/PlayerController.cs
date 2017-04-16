using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
    public enum Gender { M, F };
    public Gender gender;
    public delegate IEnumerator PlayerPowerup(PlayerController player);
    // Movement amount (for all movement directions).
    public float speed;
    // Controller number defines which controller the player is to use if keyboard controls aren't used.
    public string playerPrefix = "P1";
    // Default direction is North.
    public FacingDirection currentDirection = FacingDirection.N;


    // Direction keycodes; to be set in the scene editor.
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
	public KeyCode jumpKey;

    private Rigidbody body;
    [HideInInspector]
    public Light playerLight;
    protected Animator m_Animator;

	private bool isGrounded = true;

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
		isGrounded = true;

        var names = Input.GetJoystickNames();
        Debug.Log("Connected Joysticks:");
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
        }
    }

    // Called on load.
    void Awake()
    {
        this.body = GetComponent<Rigidbody>();
        this.playerLight = transform.GetComponentInChildren<Light>();
    }
	
	// Apply the new velocity to our animations
	void UpdatePlayerAnimations ()
	{
		if (this.body.velocity.x != 0 || this.body.velocity.y != 0) {
			m_Animator.SetBool ("IsMoving", true);
		}
		else {
			m_Animator.SetBool ("IsMoving", false);
		}
	}

	// Change the facing direction according to current velocity direction.
	void UpdateFacingDirection (float velocity_x, float velocity_y)
	{
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

	float UpdateXVelocity (float velocity_x)
	{
        float hAxis = Input.GetAxis(playerPrefix + "Horizontal");
        //print(playerPrefix + "Horizontal: " + hAxis.ToString());
        if (Input.GetKey (leftKey) || (hAxis < 0)) {
			velocity_x -= speed;
			this.currentDirection = FacingDirection.W;
		}
		else if (Input.GetKey (rightKey) || (hAxis > 0)) {
				velocity_x += speed;
				this.currentDirection = FacingDirection.E;
		}
		return velocity_x;
	}

	float UpdateYVelocity (float velocity_y)
	{
        float vAxis = Input.GetAxis(playerPrefix + "Vertical");
        //print(playerPrefix + "Vertical: " + vAxis.ToString());
        if (Input.GetKey (upKey) || (vAxis > 0)) {
			velocity_y += speed;
			this.currentDirection = FacingDirection.N;
		}
		else
		if (Input.GetKey (downKey) || (vAxis < 0)) {
			velocity_y -= speed;
			this.currentDirection = FacingDirection.S;
		}
		return velocity_y;
	}

    float HandlePlayerJumping(float velocity_z)
	{
		if( Input.GetKey(jumpKey) )
		{
			if(isGrounded == true)
			{
				velocity_z -= speed * 4;
				isGrounded = false;
			}

			if(velocity_z == 0 && isGrounded == false)
			{
				isGrounded = true;
			}

		}
		return velocity_z;
	}

    // Called once each frame.
    void Update()
    {
        // If no buttons are pressed, velocity is 0.
        float velocity_x = 0f;
        float velocity_y = 0f;
/*		float velocity_z = 0f;*/

        if(this.transform.position.z < -0.5f)
        {
            // Set y component of velocity.
            velocity_y = UpdateYVelocity(velocity_y);

            // Set x component of velocity.
            velocity_x = UpdateXVelocity(velocity_x);
            
            // Change the facing direction according to current velocity direction.
            UpdateFacingDirection(velocity_x, velocity_y);

            // Handle a scene reset if the player requests it
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SplashScreen");
            }            

            // Handle jumping behavior when user presses jumpKey
            //velocity_z = HandlePlayerJumping(this.body.velocity.z);

            // Set a new velocity normalized based on speed.
            Vector3 newVelocity = new Vector3(velocity_x, velocity_y, this.body.velocity.z).normalized * this.speed;
            newVelocity.z = this.body.velocity.z;
            //this.body.velocity += new Vector3(0f, 0f, velocity_z);
            this.body.velocity = newVelocity;
        }
        else
        {
            Vector3 pos = this.body.transform.position;
            pos.z = Mathf.Lerp(pos.z, 6.0f, Time.deltaTime / 2f);
            this.body.transform.position = pos;
            m_Animator.SetBool("IsMoving", true);
        }

        // Apply the new velocity to our animations
        UpdatePlayerAnimations ();

        // Apply our current direction to the player rotation
        HandlePlayerRotation();
    }

	// Called once per timestep.
	void FixedUpdate()
	{
		
	}

    public void GetPowerup(PlayerPowerup powerupFunction)
    {
        StartCoroutine(powerupFunction(this));
    }
}
