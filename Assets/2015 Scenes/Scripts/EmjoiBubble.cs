using UnityEngine;
using System.Collections;

public class EmjoiBubble : MonoBehaviour {
	public enum EmojiState
	{
		Normal,
		Sick,
		Together
	}
	public EmojiState State = EmojiState.Normal;
	public float AlphaFadeSpeed = 0.5f;
	public Transform TargetPlayer;
	public float OffsetX = 0.6f;
	public float OffsetY = 0.6f;
	public SpriteRenderer TogetherEmoji;
	public SpriteRenderer SicknessEmoji;

	// Use this for initialization
	void Start () 
	{
		State = EmojiState.Normal;
		this.renderer.material.color = AdjustColorAlpha (this.renderer.material.color, -100000.0f);
		TogetherEmoji.renderer.material.color = AdjustColorAlpha (TogetherEmoji.renderer.material.color, -100000.0f);
		SicknessEmoji.renderer.material.color = AdjustColorAlpha (SicknessEmoji.renderer.material.color, -100000.0f);
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
		if(hideEmoji && emoji.renderer.material.color.a > 0f)
		{
			emoji.renderer.material.color = AdjustColorAlpha (emoji.renderer.material.color, -AlphaFadeSpeed);
		}
		else if(!hideEmoji && emoji.renderer.material.color.a < 1f)
		{
			emoji.renderer.material.color = AdjustColorAlpha (emoji.renderer.material.color, AlphaFadeSpeed);
		}
	}

	// Handles fading the emoji bubble image in and out based on the current state
	void HandledFading(bool showBubble)
	{
		// Hide the speech bubble if it's supposed to be hidden.
		if(State == EmojiState.Normal)
		{
			HandleEmojiFade(TogetherEmoji, true);
			HandleEmojiFade(SicknessEmoji, true);

			if(this.renderer.material.color.a > 0f)
			{
				this.renderer.material.color = AdjustColorAlpha (this.renderer.material.color, -AlphaFadeSpeed);
			}
		}
		// Otherwise show the speech bubble if it's supposed to be shown :)
		else 
		{
			if(this.renderer.material.color.a < 1f)
			{
				this.renderer.material.color = AdjustColorAlpha (this.renderer.material.color, AlphaFadeSpeed);
			}
			if(State == EmojiState.Sick)
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
		HandledFading(State != EmojiState.Normal);
		FollowTarget();
	}
}
