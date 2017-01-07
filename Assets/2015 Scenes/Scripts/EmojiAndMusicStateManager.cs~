using UnityEngine;
using System.Collections;

public class EmojiAndMusicStateManager : MonoBehaviour {
	public sound music;
	public bool isActive = false;
	public EmjoiBubble P1EmojiBubble;
	public EmjoiBubble P2EmojiBubble;
	public PlayerController P2PlayerController;
	public float SicknessSpeed = 1.6f;


	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" )
		{
			//Debug.Log ("hey we collided for music together.");
			music.increaseVolume(1.0f);
			P1EmojiBubble.State = EmjoiBubble.EmojiState.Together;
			P2EmojiBubble.State = EmjoiBubble.EmojiState.Together;
			isActive = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{		
			P1EmojiBubble.State = EmjoiBubble.EmojiState.Normal;

			if(P2PlayerController.speed <= SicknessSpeed)
			{
				P2EmojiBubble.State = EmjoiBubble.EmojiState.Sick;
			}
			else
			{
				P2EmojiBubble.State = EmjoiBubble.EmojiState.Normal;
			}
						
			music.RemoveMusic ();
			isActive = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if((P2PlayerController.speed <= SicknessSpeed) && (P2EmojiBubble.State != EmjoiBubble.EmojiState.Together) )
		{
			P2EmojiBubble.State = EmjoiBubble.EmojiState.Sick;
		}
	}
}
