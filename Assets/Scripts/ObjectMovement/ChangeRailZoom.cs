using UnityEngine;
using System.Collections;

public class ChangeRailZoom : MonoBehaviour {

	public float NewMaxSpeed = 1.0f;
	public float NewMaxCameraSize = 48.0f;
	public float NewCameraSizeSpeed = 10.0f;
	public RailCart RailCartToModify;
	private bool m_Modified = false;

	// Use this for initialization
	void Start () {
		m_Modified = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && m_Modified == false) 
		{
			RailCartToModify.ResetRailcartSettings( NewMaxSpeed, NewMaxCameraSize, NewCameraSizeSpeed );
			m_Modified = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
