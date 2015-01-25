using UnityEngine;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

	public MonoBehaviour Player1;
	public MonoBehaviour Player2;
	public Material WhiteQuad;
	public Material WhatDoWeDoNow;

	// Use this for initialization
	void Start () 
	{
		/*Color originalColour = WhiteQuad.color;
		WhiteQuad.color = new Color(originalColour.r, originalColour.g, originalColour.b, 0);
		originalColour = WhatDoWeDoNow.color;
		WhatDoWeDoNow.color = new Color(originalColour.r, originalColour.g, originalColour.b, 0);
		*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			if(Player2 == null)
			{
				// Fade out primary music
				// Fade in blank display
				// Player Just guy song
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
}
