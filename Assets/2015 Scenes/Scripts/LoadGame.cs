using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	public sound music;
	// Use this for initialization
	void Start () {
		music.increaseVolume(0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			if (music.audio.volume > 0.4f)
			{
				music.RemoveMusic();
			}
			else
			{
				Application.LoadLevel("FinalScene");
			}
		}
	}
}