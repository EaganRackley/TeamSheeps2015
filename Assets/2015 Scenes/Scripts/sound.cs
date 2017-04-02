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

	public void RemoveMusic() {
		m_hearingSound = false;
	}

	public void PlayDeathMusic() {
		//todo: add logic to play the death music for the player
	}

	public void PlayMainTheme() {
		//todo: play the main theme when game loads
	}

	public void PlayApartMusic() {
		//todo: when players are apart play music here
	}

	public void PlayTogetherMusic() {
		//todo: when players are together play this music here
	}

    public void fadeMusic()
    {
        if(m_maxSoundVolume > 0 )
        {
            m_maxSoundVolume = Mathf.Lerp(m_maxSoundVolume, 0, 10.0f * Time.deltaTime);
        }
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
		else if (m_hearingSound == false) {
			if (m_AudioSrc.volume > 0.0f) {
				m_AudioSrc.volume -= .01f;
			}
		}
	}
}
