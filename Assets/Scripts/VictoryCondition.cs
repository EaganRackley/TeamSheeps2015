using UnityEngine;
using System.Collections;

public class VictoryCondition : MonoBehaviour {
	public moveWorld m_moveWorld;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" )
		{
			m_moveWorld.RotateCameraTo(180);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
