using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {
	private Vector2 velocity;
	public float smoothTime = 0.15f;
	public Transform target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = this.transform.position;
		pos.x = Mathf.SmoothDamp( pos.x, target.position.x, ref(velocity.x), smoothTime);
		pos.y = Mathf.SmoothDamp( pos.y, target.position.y, ref(velocity.y), smoothTime);
		this.transform.position = pos;
	}
}
