using UnityEngine;
using System.Collections;

public class moveCube : MonoBehaviour {
	public moveWorld m_moveWorld;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = -1;
		this.transform.position += ( Vector3.up * h * Time.deltaTime );
		if (this.transform.position.y  < -1 && this.transform.position.y > -1.03 ) {
			m_moveWorld.RotateCameraTo(180);
		}

	}
}
