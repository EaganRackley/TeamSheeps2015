using UnityEngine;
using System.Collections;

public class moveWorld : MonoBehaviour {

	public int moveDegree = 15;
	private float m_TargetAngle = 0;
	private bool m_RotatingCamera = false;

	// Use this for initialization
	void Start () {
	
	}

	public void RotateCameraTo(float targetAngle)
	{
		m_TargetAngle = targetAngle;
		m_RotatingCamera = true;

	}

	float angle = 0;
	
	// Update is called once per frame
	void Update () {
		if ( m_RotatingCamera == true ) {
			if (this.angle < m_TargetAngle ) {
				angle += moveDegree * Time.deltaTime;
				transform.eulerAngles = new Vector3( 0, 0, angle );
			}
		}
	
	}
}
