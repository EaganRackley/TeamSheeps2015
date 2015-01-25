using UnityEngine;
using System.Collections;

public class MusicTogether : MonoBehaviour {
	public sound music;
	public bool isActive = false;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player2" )
		{
			Debug.Log ("I entered shit here!!!");
			//music.RemoveMusic();
			music.increaseVolume(1.0f);
			isActive = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		music.RemoveMusic();
		isActive = false;
	}

	// Update is called once per frame
	void Update () {
	}
}
