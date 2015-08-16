using UnityEngine;
using System.Collections;

public class EmjoiBubble : MonoBehaviour {
	public enum EmojiState
	{
		Normal,
		Sick,
		Together,
		Dead
	}
	public EmojiState InitialState = EmojiState.Normal;
	private EmojiState myState = EmojiState.Normal;
	public float AlphaFadeSpeed = 0.5f;
	public PlayerController TargetPlayer;
	public float OffsetX = 0.6f;
	public float OffsetY = 0.6f;
	public SpriteRenderer TogetherEmoji;
	public SpriteRenderer SicknessEmoji;

	/// <summary>
	/// Gets or sets the current emoji state as long as the TargetPlayer isn't dead.
	/// </summary>
	/// <value>The state.</value>
	public EmojiState State {
		get {
			return myState;
		}
		set {
			if(TargetPlayer != null)
			{
				myState = value;
			}
			else
			{
				myState = EmojiState.Dead;
			}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		myState = InitialState;
		this.GetComponent<Renderer>().material.color = AdjustColorAlpha (this.GetComponent<Renderer>().material.color, -100000.0f);
		TogetherEmoji.GetComponent<Renderer>().material.color = AdjustColorAlpha (TogetherEmoji.GetComponent<Renderer>().material.color, -100000.0f);
		SicknessEmoji.GetComponent<Renderer>().material.color = AdjustColorAlpha (SicknessEmoji.GetComponent<Renderer>().material.color, -100000.0f);
	}

	// Adjust the alpha of the specified color and returns it
	Color AdjustColorAlpha (Color color, float fadeSpeed)
	{
		Color newColor = color;
		newColor.a += fadeSpeed * Time.deltaTime;
		if(fadeSpeed > 0 && newColor.a >= 1f)
		{
			newColor.a = 1f;
		}
		else if(fadeSpeed <= 0 && newColor.a <= 0f)
		{
			newColor.a = 0f;
		}

		return newColor;
	}

	void HandleEmojiFade(SpriteRenderer emoji, bool hideEmoji)
	{
		if(hideEmoji && emoji.GetComponent<Renderer>().material.color.a > 0f)
		{
			emoji.GetComponent<Renderer>().material.color = AdjustColorAlpha (emoji.GetComponent<Renderer>().material.color, -AlphaFadeSpeed);
		}
		else if(!hideEmoji && emoji.GetComponent<Renderer>().material.color.a < 1f)
		{
			emoji.GetComponent<Renderer>().material.color = AdjustColorAlpha (emoji.GetComponent<Renderer>().material.color, AlphaFadeSpeed);
		}
	}

	// Handles fading the emoji bubble image in and out based on the current state
	void HandleFading(bool showBubble)
	{
		// Hide the speech bubble if it's supposed to be hidden.
		if(myState == EmojiState.Normal)
		{
			HandleEmojiFade(TogetherEmoji, true);
			HandleEmojiFade(SicknessEmoji, true);

			if(this.GetComponent<Renderer>().material.color.a > 0f)
			{
				this.GetComponent<Renderer>().material.color = AdjustColorAlpha (this.GetComponent<Renderer>().material.color, -AlphaFadeSpeed);
			}
		}
		else if(myState == EmojiState.Dead)
		{
			if(this.GetComponent<Renderer>().material.color.a > 0f)
			{
				HandleEmojiFade(TogetherEmoji, true);
				HandleEmojiFade(SicknessEmoji, true);

				this.GetComponent<Renderer>().material.color = AdjustColorAlpha (this.GetComponent<Renderer>().material.color, -AlphaFadeSpeed);
			}
		}
		// Otherwise show the speech bubble if it's supposed to be shown :)
		else 
		{
			if(this.GetComponent<Renderer>().material.color.a < 1f)
			{
				this.GetComponent<Renderer>().material.color = AdjustColorAlpha (this.GetComponent<Renderer>().material.color, AlphaFadeSpeed);
			}
			if(myState == EmojiState.Sick)
			{
				HandleEmojiFade(TogetherEmoji, true);
				HandleEmojiFade(SicknessEmoji, false);
			}
			else
			{
				HandleEmojiFade(TogetherEmoji, false);
				HandleEmojiFade(SicknessEmoji, true);

			}
		}
	}

	// Follows the TargetPlayer transform
	void FollowTarget()
	{
		if(TargetPlayer != null )
		{
			Vector3 pos = TargetPlayer.transform.position;
			pos.x += OffsetX;
			pos.y += OffsetY;
			this.transform.position = pos;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(TargetPlayer != null)
		{
			HandleFading(myState != EmojiState.Normal);
			FollowTarget();
		}
		else
		{
			myState = EmojiState.Dead;
			HandleFading(false);
		}

	}
}
