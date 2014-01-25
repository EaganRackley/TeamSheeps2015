using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	bool bMovementAllowed = false;
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	private BoxCollider2D boxCollider;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded =  Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		anim.SetBool("Grounded", grounded);

		//adjust the box collider to be the same size as the sprite
		boxCollider.size = ((SpriteRenderer)renderer).sprite.bounds.size;
		//deflate the size a bit 
		boxCollider.size = new Vector2(.7f * boxCollider.size.x, .8f * boxCollider.size.y);
		boxCollider.center = ((SpriteRenderer)renderer).sprite.bounds.center;

		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetButtonDown("Jump") && grounded)
		    jump = true;
	}

	void FixedUpdate()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		//Debug.Log("Speed: " + Mathf.Abs(h));
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		//if(!bMovementAllowed)
		//	return;
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed)
		{
			Debug.Log("moving " + Vector2.right * h * moveForce);
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		}

		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
		{
			Debug.Log("speed" + rigidbody2D.velocity);
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !facingRight)
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && facingRight)
			Flip();

		// If the player should jump...
		if (jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			jump = false;
		}

		
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void EnableControls()
	{
		bMovementAllowed = true;
	}
}
