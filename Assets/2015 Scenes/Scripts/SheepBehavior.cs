using UnityEngine;
using System.Collections;
					
public class SheepBehavior : MonoBehaviour {

	public Vector3 rotation;
	public float JumpHeight = 1.0f;
    public float JumpTimer = 0.0f;
	private const float MAX_TIMER = 1.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 JumpTimer += 0.5f * Time.deltaTime;
		 if(JumpTimer > MAX_TIMER ) {
			JumpTimer = 0f;
			Vector3 vel = this.GetComponent<Rigidbody>().velocity;
			vel.z = -5f;
			this.GetComponent<Rigidbody>().velocity = vel;
		}
		//rotation.z =  rotation.z + 0.01f;
		//this.transform.Rotate(rotation);
		//this.transform.Rotate(new Vector3(0.0f, 0.0f, = otation.z));
	}
}
