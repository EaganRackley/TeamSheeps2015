using UnityEngine;
using System.Collections;

public class MusicZone : MonoBehaviour {
	public sound music;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" )
		{
			music.increaseVolume(1.0f);			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}