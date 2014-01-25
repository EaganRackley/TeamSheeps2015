using UnityEngine;
using System.Collections;

public class PlayerThingPartDeux : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]

	public bool bMovementAllowed = false;
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public bool jump = false;				// Condition for whether the player should jump.

	private BoxCollider2D boxCollider;
	private bool grounded;
	private Transform groundCheck;
	private Animator anim;

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
		anim.SetBool("Grounded", grounded);

		boxCollider.size = ((SpriteRenderer)renderer).sprite.bounds.size;
		//deflate the size a bit 
		boxCollider.size = new Vector2(.7f * boxCollider.size.x, .8f * boxCollider.size.y);
		boxCollider.center = ((SpriteRenderer)renderer).sprite.bounds.center;

		if (Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}

	void FixedUpdate ()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (!bMovementAllowed)
			return;

		float h = Input.GetAxis("Horizontal");

		anim.SetFloat("Speed", Mathf.Abs(h));
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		rigidbody2D.velocity = new Vector2(h * maxSpeed, rigidbody2D.velocity.y);

		if (h > 0 && !facingRight)
			Flip();
		else if (h < 0 && facingRight)
			Flip();

		if (jump)
		{
			// Set the Jump animator trigger parameter.
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
