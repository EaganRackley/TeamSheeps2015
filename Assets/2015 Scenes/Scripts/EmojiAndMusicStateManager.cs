using UnityEngine;
using System.Collections;

public class EmojiAndMusicStateManager : MonoBehaviour {
	public sound music;
	public bool isActive = false;
	public EmjoiBubble P1EmojiBubble;
	public EmjoiBubble P2EmojiBubble;


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
						P2EmojiBubble.State = EmjoiBubble.EmojiState.Normal;
						music.RemoveMusic ();
						isActive = false;
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
