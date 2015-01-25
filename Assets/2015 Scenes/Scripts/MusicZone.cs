using UnityEngine;
using System.Collections;

public class MusicZone : MonoBehaviour {
	public sound music;
	
	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		//todo Add Trigger for each zone respectfully
		if (other.gameObject.tag == "Player" )
		{
			music.increaseVolume(1.0f);			
		}
		if (other.gameObject.tag == "Player2")
		{
			music.increaseVolume(0.5f);
		}
	}

	void OnCollisionExit(Collision other)
	{
		music.RemoveMusic();
	}
	//void OnTriggerEnter2D(Collider2D other)
	//{
		//todo Add Trigger for each zone respectfully
		//if (other.gameObject.tag == "Player2" )
		//{
			//music.increaseVolume(1.0f);			
		//}
	//}
	
	// Update is called once per frame
	void Update () {
		
	}
}