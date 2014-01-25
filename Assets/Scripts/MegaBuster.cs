using UnityEngine;
using System.Collections;

public class MegaBuster : MonoBehaviour {

	public Rigidbody2D blaster;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.


	private PlayerThingPartDeux playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerThingPartDeux>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerCtrl.bMovementAllowed && Input.GetButtonDown("Fire1"))
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Fire");
			//audio.Play();

			// If the player is facing right...
			if (playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(blaster, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else
			{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(blaster, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
				
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
		}
	}
}
