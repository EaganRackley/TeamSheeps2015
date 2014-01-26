using UnityEngine;
using System.Collections;

public class FollowCameraScript : TransitionObject {

	public Transform myCameraTransform;
	public float myFollowSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		pos.x = Mathf.Lerp(pos.x, myCameraTransform.position.x, myFollowSpeed * Time.deltaTime);
		pos.y = Mathf.Lerp(pos.y, myCameraTransform.position.y, myFollowSpeed * Time.deltaTime);		
		this.transform.position = pos;	
	}
}
