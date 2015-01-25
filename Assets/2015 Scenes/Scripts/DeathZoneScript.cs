using UnityEngine;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

	public MonoBehaviour Player1;
	public MonoBehaviour Player2;
	public MeshRenderer WhiteQuad;
	public MeshRenderer WhatDoWeDoNow;

	bool m_EndingTriggered = false;
	

	// Use this for initialization
	void Start () 
	{
		// Set both materials to transparent so we can play the game.
		Color newColor = WhiteQuad.renderer.material.color;
		newColor.a = 0;
		WhiteQuad.renderer.material.color = newColor;

		newColor = WhatDoWeDoNow.renderer.material.color;
		newColor.a = 0;
		WhatDoWeDoNow.renderer.material.color = newColor;
	}

	// Handles fading in the white quad
	bool HandledWhiteFadeIn()
	{
		if(WhiteQuad.renderer.material.color.a < 1f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = WhiteQuad.renderer.material.color;
			newColor.a += 0.1f * Time.deltaTime;
			WhiteQuad.renderer.material.color = newColor;
			return false;
		}
		return true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			if(Player2 == null)
			{
				m_EndingTriggered = true;
			}
			else
			{
				// Play guy and girl song
			}
		} 
		else if (other.tag == "Player2") 
		{
			DestroyObject(other.gameObject);
		}
	}

	void Update()
	{
		if (m_EndingTriggered == true) 
		{	
			// Fade out primary music
			// Fade in blank display
			// Player Just guy song or both
			if( HandledWhiteFadeIn() )
			{

			}
		}
	}
}
