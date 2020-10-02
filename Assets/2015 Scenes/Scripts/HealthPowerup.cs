using UnityEngine;
using System.Collections;

public class HealthPowerup : AbstractPowerup
{
    public float HealthIncrease;
    public AudioSource TogetherTheme;
    private bool m_faded = false;
    private float m_fadeTime = 1.5f;
    private EventManager m_eventManager;

    private void Start()
    {
        m_eventManager = FindObjectOfType<EventManager>();
    }

    public override IEnumerator PowerupFunction(PlayerController player)
    {
        while(HealthIncrease > 0f)
        {
            player.speed += Time.deltaTime;
            HealthIncrease -= Time.deltaTime;
            if (TogetherTheme != null && TogetherTheme.GetComponent<AudioSource>().volume < 1f)
                TogetherTheme.GetComponent<AudioSource>().volume += (6.0f * Time.deltaTime);
            //if (!player.HealingParticles().isPlaying)
            //    player.HealingParticles().Play();
            yield return new WaitForSeconds(0.16f);
        }

        //float timeout = 3f;
        //while(timeout > 0f)
        //{
        //    timeout -= Time.deltaTime;
        yield return new WaitForSeconds(2f);
        //    if (TogetherTheme != null && TogetherTheme.GetComponent<AudioSource>().volume < 1f)
        //        TogetherTheme.GetComponent<AudioSource>().volume += (0.2f * Time.deltaTime);
        //}


        if (TogetherTheme != null)
        { 
            while (TogetherTheme.GetComponent<AudioSource>().volume > 0f)
            {
                TogetherTheme.GetComponent<AudioSource>().volume -= (2f * Time.deltaTime);
                yield return new WaitForSeconds(0.1f);
            }
        }
        //if (player.HealingParticles().isPlaying)
        //    player.HealingParticles().Stop();

    }
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player2" && m_faded == false)
        {
            m_eventManager.Score++;
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            player.GetPowerup(this.PowerupFunction);
            m_faded = true;
            GetComponentInChildren<ParticleSystem>().Play();
            //Destroy(this.gameObject);
        }
    }

    public void Update()
    {
        
        if (m_faded && m_fadeTime > 0f)
        {
            m_fadeTime -= Time.deltaTime;

            Color col = this.GetComponent<SpriteRenderer>().color;
            if (m_fadeTime > 1f)
                col.a = 1f;
            else
                col.a = m_fadeTime;
            this.GetComponent<SpriteRenderer>().color = col;
        }
        else if (m_faded)
        {
            if (this.GetComponentInChildren<Light>().intensity > 0f)
                this.GetComponentInChildren<Light>().intensity -= Time.deltaTime;
            if (this.GetComponentInChildren<Light>().intensity <= 0f)
                GameObject.Destroy(this);
        }
    }

}
