using UnityEngine;
using System.Collections;

public class DisappearingShadow : MonoBehaviour {

	public Light fadeLight;

	// Make sure our alpha is at max so that we can fade out the shadow player
	void Start () {
		Color initColor = this.renderer.material.color;
		initColor.a = 1.0f;
		this.renderer.material.color = initColor;
		fadeLight.intensity = 1.0f;
	}

	// Handles fading in the white quad
	bool HandledFadeAway()
	{
		if(this.renderer.material.color.a > 0f)
		{
			// Set both materials to transparent so we can play the game.
			Color newColor = this.renderer.material.color;
			newColor.a -= 0.05f * Time.deltaTime;
			this.renderer.material.color = newColor;
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
