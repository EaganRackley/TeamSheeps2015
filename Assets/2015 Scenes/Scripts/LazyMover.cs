using UnityEngine;
using System.Collections;

public class LazyMover : MonoBehaviour {
	public Vector3 startVelocity = new Vector3(1f, 1f, 0f);
	public Vector3 targetPoint;

	public float distanceTraveled = 0f;
	public float jitterFactor = 7f;

	private Rigidbody body;
	
	// Use this for initialization
	void Start() {
		this.body = GetComponent<Rigidbody>();
		this.body.velocity = startVelocity;

		// Target point starts at an offset from the current position.
		this.targetPoint = this.body.position + new Vector3(1, 1, 0);
	}
	
	// Update is called once per frame
	void Update() {
		// Update with distance traveled.
		this.distanceTraveled += this.body.velocity.magnitude * Time.deltaTime;
	}
	
	// Called once per timestep.
	void FixedUpdate() {
		// Lazy movers experience a (unit-strength) force towards the target point.
		Vector3 unitTowards = (targetPoint - this.body.position).normalized;
		this.body.AddForce(unitTowards);

		// Every so many units traveled, flip the velocity and determine a new target point randomly.
		if (distanceTraveled >= jitterFactor) {
			this.body.velocity *= -1;

			float theta = Random.Range(0f, 1f) * 2 * Mathf.PI;
			this.targetPoint = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f) + this.body.position;
			this.distanceTraveled = 0f;
        }
    }
}
