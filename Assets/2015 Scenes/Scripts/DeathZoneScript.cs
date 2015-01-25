using UnityEngine;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

	public MonoBehaviour Player1;
	public MonoBehaviour Player2;
	public MeshRenderer WhiteQuad;
	public MeshRenderer WhatDoWeDoNow;
	public sound MainTheme;
	public sound TogetherTheme;
	public sound DeathThemeTogether;
	public sound DeathThemeBoyAlone;

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

	// Fades out the main theme
	bool HandleMusicFade()
	{
		if (MainTheme.audio.volume > 0.1f)
		{
			MainTheme.audio.volume -= (1f);
			TogetherTheme.audio.volume -= (1f);
			return false;
		}	
		MainTheme.audio.Stop ();
		TogetherTheme.audio.Stop ();
		//DeathThemeTogether.audio.Play
		return true;
	}

	// Handles fading in the white quad
	bool HandledWhiteFadeIn()
	{
		if(WhiteQuad.renderer.material.color.a < 1f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = WhiteQuad.renderer.material.color;
			newColor.a += 0.25f * Time.deltaTime;
			WhiteQuad.renderer.material.color = newColor;
			return false;
		}
		return true;
	}

	bool HandledTextFadeIn()
	{
		if(WhatDoWeDoNow.renderer.material.color.a < 1f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = WhatDoWeDoNow.renderer.material.color;
			newColor.a += 0.25f * Time.deltaTime;
			WhatDoWeDoNow
				.renderer.material.color = newColor;
			return false;
		}
		return true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			m_EndingTriggered = true;
			
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
			HandleMusicFade();
			// Fade in blank display
			if( HandledWhiteFadeIn() )
			{
				// Fase in text
				if( HandledTextFadeIn() )
				{
				}
			}
		}
	}
}
