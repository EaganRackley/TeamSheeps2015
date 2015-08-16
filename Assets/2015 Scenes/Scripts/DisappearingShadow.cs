using UnityEngine;
using System.Collections;

public class DisappearingShadow : MonoBehaviour {

	public Light fadeLight;
	public Transform targetPlayer;
	[HideInInspector]
	protected Animator m_Animator;


	// Make sure our alpha is at max so that we can fade out the shadow player
	void Start () {
		Color initColor = this.GetComponent<Renderer>().material.color;
		initColor.a = 1.0f;
		this.GetComponent<Renderer>().material.color = initColor;
		fadeLight.intensity = 1.0f;
		m_Animator = GetComponent<Animator>();
	}

	// Handles fading in the white quad
	bool HandledFadeAway()
	{
		if(this.GetComponent<Renderer>().material.color.a > 0f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = this.GetComponent<Renderer>().material.color;
			newColor.a -= 0.05f * Time.deltaTime;
			this.GetComponent<Renderer>().material.color = newColor;
			fadeLight.intensity = newColor.a;
			return false;
		}
		return true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(HandledFadeAway() == true)
		{
			Destroy(this.gameObject);
		}	
	}
}
