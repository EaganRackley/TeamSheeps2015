using UnityEngine;
using System.Collections;

public class FrogMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingLeft = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.

	private BoxCollider2D boxCollider;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		anim.SetFloat("hInput", Mathf.Abs(h));
		anim.SetFloat("vInput", v);

		rigidbody2D.velocity = new Vector2(h * maxSpeed, v * maxSpeed);

		if (h < 0 && !facingLeft)
			Flip();
		else if (h > 0 && facingLeft)
			Flip();
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingLeft = !facingLeft;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
