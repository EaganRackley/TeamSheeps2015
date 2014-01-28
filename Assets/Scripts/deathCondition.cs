using UnityEngine;
using System.Collections;

public class deathCondition : MonoBehaviour {
	private GameObject m_PlayerObject;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" )
		{
			m_PlayerObject = other.gameObject;
			m_PlayerObject.GetComponent<PlayerController>().deathState = true;
			m_PlayerObject.GetComponent<PlayerController>().deathTimer = 0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
