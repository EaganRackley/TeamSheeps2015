using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int health = 2;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private bool dead = false;			// Whether or not the enemy is dead.

	// Use this for initialization
	void Awake () {
		ren = transform.Find("body").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
			Death();
	}

	public void Hurt(int damage)
	{
		//do the damage here
		health -= damage;
	}

	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach (SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		//ren.enabled = true;
		//ren.sprite = deadEnemy;

		// Increase the score by 100 points
		//score.score += 100;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		rigidbody2D.fixedAngle = false;
		//rigidbody2D.AddTorque(Random.Range(deathSpinMin, deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach (Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
		//int i = Random.Range(0, deathClips.Length);
		//AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		//Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}


	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
