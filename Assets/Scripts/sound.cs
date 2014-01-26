using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class sound : MonoBehaviour {

	private bool m_hearingSound = false;
	private float m_maxSoundVolume = 0;
	private AudioSource m_AudioSrc;
	// Use this for initialization
	void Start () {
		m_AudioSrc = GetComponent<AudioSource>();
	
	}

	public void increaseVolume(float maxSoundVolume) {
		m_maxSoundVolume = maxSoundVolume;
		m_hearingSound = true;
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	void FixedUpdate() {
		if ( m_hearingSound == true ) {
			if (m_AudioSrc.volume < 1.0f) {
				m_AudioSrc.volume += .01f;
			}
		}
	}
}
