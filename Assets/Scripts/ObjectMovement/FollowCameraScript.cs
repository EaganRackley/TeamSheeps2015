using UnityEngine;
using System.Collections;

public class FollowCameraScript : MonoBehaviour{

	public Transform myCameraTransform;
	public float myFollowSpeed = 1.0f;
	public bool Enabled = true;

	void EnableFollow()
	{
		Enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (Enabled == true) 
		{
			Vector3 pos = this.transform.position;
			pos = Vector3.MoveTowards (pos, myCameraTransform.position, myFollowSpeed * Time.fixedDeltaTime);
						//pos.x = Mathf.Lerp(pos.x, myCameraTransform.position.x, myFollowSpeed * Time.deltaTime);
						//pos.y = Mathf.Lerp(pos.y, myCameraTransform.position.y, myFollowSpeed * Time.deltaTime);		
			pos.z = this.transform.position.z;
			this.transform.position = pos;	
		}
	}
}
