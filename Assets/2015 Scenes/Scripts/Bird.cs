using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	public float turnProb = .05f;
	public float maxTurnRadians = 30f;

	public Vector3 startVelocity = new Vector3(1f, 1f, 0f);
	public Vector3 targetPoint = new Vector3(0f, 0f, 2f);

	private Rigidbody body;

	// Use this for initialization
	void Start() {
		this.body = GetComponent<Rigidbody>();
		this.body.velocity = startVelocity;
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	// Called once per timestep.
	void FixedUpdate() {
		// Birds experience a (unit-strength) force towards the target point.
		Vector3 unitTowards = (targetPoint - this.body.position).normalized;
		this.body.AddForce(unitTowards);

		// With some random chance of a turn.
		if (Random.Range(0f, 1f) < turnProb) {
			this.body.transform.Rotate(new Vector3(0f, -maxTurnRadians * Random.Range(0f, 1f), 0f));
		}
	}
}
