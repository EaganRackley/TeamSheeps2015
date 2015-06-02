using UnityEngine;
using System.Collections;

public class MusicApart : MonoBehaviour {
	public EmojiAndMusicStateManager m_MusicTogether;
	public sound music;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (m_MusicTogether.isActive == false)
		{
			music.increaseVolume(1.0f);
		}	
	}
}
