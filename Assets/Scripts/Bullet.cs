using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If it hits an enemy...
		if (col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt(1);

			// Call the explosion instantiation.
			//OnExplode();

			// Destroy the rocket.
			Destroy(gameObject);
		}
		// Otherwise if it hits a bomb crate...
		
		// Otherwise if the player manages to shoot himself...
		else if (col.gameObject.tag != "Player")
		{
			// Instantiate the explosion and destroy the rocket.
			//OnExplode();
			Destroy(gameObject);
		}
	}
}
