﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathZoneScript : MonoBehaviour {

    public string SceneToLoad = "TitleScreen2020.unity";
    public MonoBehaviour Player1;
	public MonoBehaviour Player2;
	public MeshRenderer WhiteQuad;
	public MeshRenderer WhatDoWeDoNow;
	public AudioSource MainTheme;
	public AudioSource TogetherTheme;
	public AudioSource DeathThemeTogether;

	public bool m_EndingSongPlayed = false;
	bool m_EndingTriggered = false;
	

	// Use this for initialization
	void Start () 
	{
		// Set both materials to transparent so we can play the game.
		Color newColor = WhiteQuad.GetComponent<Renderer>().material.color;
		newColor.a = 1;
		WhiteQuad.GetComponent<Renderer>().material.color = newColor;

		newColor = WhatDoWeDoNow.GetComponent<Renderer>().material.color;
		newColor.a = 0;
		WhatDoWeDoNow.GetComponent<Renderer>().material.color = newColor;
	}

	// Fades out the main theme
	bool HandleMusicFade()
	{
		if(!DeathThemeTogether.GetComponent<AudioSource>().isPlaying && !m_EndingSongPlayed) {            
            DeathThemeTogether.GetComponent<AudioSource>().PlayOneShot(DeathThemeTogether.clip);
			m_EndingSongPlayed = true;
		}

		if (MainTheme.GetComponent<AudioSource>().volume > 0.1f)
		{
			if(MainTheme != null)
				MainTheme.GetComponent<AudioSource>().volume -= (0.2f * Time.deltaTime);
			if(TogetherTheme != null)
				TogetherTheme.GetComponent<AudioSource>().volume -= (0.2f * Time.deltaTime);
			return false;
		}

		return true;
	}

	// Handles fading in the white quad
	bool HandledWhiteFadeIn()
	{
		if(WhiteQuad.GetComponent<Renderer>().material.color.a < 1f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = WhiteQuad.GetComponent<Renderer>().material.color;
			newColor.a += 0.25f * Time.deltaTime;
			WhiteQuad.GetComponent<Renderer>().material.color = newColor;
			return false;
		}
		return true;
	}

    // Handles fading out the white quad
    void HandledWhiteFadeOut()
    {
        if (WhiteQuad.GetComponent<Renderer>().material.color.a > 0f)
        {
            // Set both materials to transparent so we can play the game.
            Color newColor = WhiteQuad.GetComponent<Renderer>().material.color;
            newColor.a -= 0.25f * Time.deltaTime;
            WhiteQuad.GetComponent<Renderer>().material.color = newColor;
        }
    }

    bool HandledTextFadeIn()
	{
		if (DeathThemeTogether.GetComponent<AudioSource>().isPlaying == false)
						DeathThemeTogether.GetComponent<AudioSource>().Play ();

		if(WhatDoWeDoNow.GetComponent<Renderer>().material.color.a < 1f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = WhatDoWeDoNow.GetComponent<Renderer>().material.color;
			newColor.a += 0.25f * Time.deltaTime;
			WhatDoWeDoNow
				.GetComponent<Renderer>().material.color = newColor;
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
            if (!DeathThemeTogether.GetComponent<AudioSource>().isPlaying && m_EndingSongPlayed == true)
            {
                SceneManager.LoadScene(SceneToLoad);
            }
            // Fade out primary music
            HandleMusicFade();
            // Fade in blank display
            if (HandledWhiteFadeIn())
            {
                // Fase in text
                if (HandledTextFadeIn())
                {
                }
            }
        }
        else
        {
            HandledWhiteFadeOut();
        }
	}
}
