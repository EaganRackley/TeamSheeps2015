using UnityEngine;
using System.Collections;

public class FollowCameraScript : MonoBehaviour{

	public SmoothCamera2D myCameraTransform;
	public float myFollowSpeed = 1.0f;
	public bool Enabled = true;
	public bool ChangeSize = false;

	private Vector3 m_BaseSize;

	void EnableFollow()
	{
		Enabled = true;
		ChangeSize = true;
		m_BaseSize = this.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		if (Enabled == true) 
		{
			Vector3 pos = this.transform.position;
			pos = Vector3.MoveTowards (pos, myCameraTransform.transform.position, myFollowSpeed * Time.fixedDeltaTime);
						//pos.x = Mathf.Lerp(pos.x, myCameraTransform.position.x, myFollowSpeed * Time.deltaTime);
						//pos.y = Mathf.Lerp(pos.y, myCameraTransform.position.y, myFollowSpeed * Time.deltaTime);		
			pos.z = this.transform.position.z;
			this.transform.position = pos;	

			if ( (ChangeSize) && (myCameraTransform.camera.orthographicSize + (m_BaseSize.x * 3.0f) != this.transform.localScale.x) )
			{
				Vector3 scale = this.transform.localScale;
				scale.x = myCameraTransform.camera.orthographicSize + (m_BaseSize.x * 3.0f);
				scale.y = myCameraTransform.camera.orthographicSize + (m_BaseSize.y * 3.0f);
				this.transform.localScale = scale;
			}
		}


	}
}
