using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]

	public bool bMovementAllowed = true;
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
    public float runMultiplyer = 2f;
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public bool jump = false;				// Condition for whether the player should jump.

    public float minSize = 0.5f;
    public float maxSize = 1.4f;
    public float sizeChangeDelta = .1f;
	
	public float groundCheckOffset = 0.2f;

	private CircleCollider2D circCollider;
	private bool grounded;
	private Transform groundCheck;
//	private Animator anim;
    private ArrayList particles;
	
 

	void Awake()
	{
		// Setting up references.
        //groundCollision = new ArrayList(3);
		particles = new ArrayList(transform.childCount);
		for(int i = 0; i < transform.childCount;++i)
		{
			particles.Add(transform.GetChild(i));
		}
      //  groundCollision.Add(gameObject.transform.parent.gameObject.transform.Find("GroundCheckL"));
       // groundCollision.Add(gameObject.transform.parent.gameObject.transform.Find("GroundCheckC"));
        //groundCollision.Add(gameObject.transform.parent.gameObject.transform.Find("GroundCheckR"));
		//groundCheck = transform.Find("groundCheck");
		groundCheck = GameObject.FindGameObjectWithTag("GroundCheckPlayer").transform;
		circCollider = GetComponent<CircleCollider2D>();
//		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//        anim.SetBool("Grounded", grounded);
		
        //this will need to be updated
        //boxCollider.size = ((SpriteRenderer)renderer).sprite.bounds.size;
		//deflate the size a bit 
		//boxCollider.size = new Vector2(.7f * boxCollider.size.x, .8f * boxCollider.size.y);
		//boxCollider.center = ((SpriteRenderer)renderer).sprite.bounds.center;

        //Debug.Log(((SpriteRenderer)renderer).sprite.bounds.min + " " + ((SpriteRenderer)renderer).sprite.bounds.max);


        
        //    //((SpriteRenderer)renderer).sprite.bounds.min.x, ((Transform)groundCollision[0]).position.y, 0);
        //((Transform)groundCollision[0]).position.Set(((SpriteRenderer)renderer).sprite.bounds.min.x, ((Transform)groundCollision[0]).position.y, 0);
        //((Transform)groundCollision[2]).position.Set(((SpriteRenderer)renderer).sprite.bounds.max.x, ((Transform)groundCollision[0]).position.y, 0);
		
		groundCheck.transform.position = ( new Vector3(transform.position.x ,
        						 transform.position.y - circCollider.radius * transform.localScale.x - groundCheckOffset ,
        						 0));

		if (Input.GetButtonDown("Jump") && grounded)
			jump = true;
        if (Input.GetButton("Run"))
            Shrink();
        else
            Grow();
		
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			Spawn();
		}
	}

	void FixedUpdate ()
	{
		
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            
		//if (!bMovementAllowed)
		//	return;
		
		float h = Input.GetAxis("Horizontal");

        if (Input.GetButton("Run"))
        {
            h *= runMultiplyer;
            
        }
		
		rigidbody2D.velocity = new Vector2(h * maxSpeed, rigidbody2D.velocity.y);

        //if (h > 0 && !facingRight)
        //    Flip();
        //else if (h < 0 && facingRight)
        //    Flip();

		if (jump)
		{
			// Set the Jump animator trigger parameter.
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));

			jump = false;
		}
	}

        void Shrink()
    {
        if(transform.localScale.x > minSize)
		{
			transform.localScale -= new Vector3(sizeChangeDelta * Time.deltaTime,
				sizeChangeDelta * Time.deltaTime,	
				0);
			foreach(Transform part in particles)
			{
				part.GetComponent<SpringJoint2D>().distance -= sizeChangeDelta *Time.deltaTime;
			}
		}
    }

    void Grow()
    {
        if(transform.localScale.x < maxSize)
		{
			transform.localScale += new Vector3(sizeChangeDelta * Time.deltaTime,
				sizeChangeDelta * Time.deltaTime,	
				0);	
			
			foreach(Transform part in particles)
			{
				part.GetComponent<SpringJoint2D>().distance += sizeChangeDelta *Time.deltaTime;
			}
		}
    }
	
	void Spawn()
	{
		transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
		rigidbody2D.velocity = new Vector3();
		transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;	
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
