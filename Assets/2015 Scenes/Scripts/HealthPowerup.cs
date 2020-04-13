using UnityEngine;
using System.Collections;

public class HealthPowerup : AbstractPowerup
{
    public float HealthIncrease;

    private bool m_faded = false;
    private float m_fadeTime = 2.5f;
    
    public override IEnumerator PowerupFunction(PlayerController player)
    {
        while(HealthIncrease > 0f)
        {
            player.speed += Time.deltaTime;
            HealthIncrease -= Time.deltaTime;
            //if (!player.HealingParticles().isPlaying)
            //    player.HealingParticles().Play();
            yield return new WaitForSeconds(0.16f);
        }
        //if (player.HealingParticles().isPlaying)
        //    player.HealingParticles().Stop();

    }
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player2" && m_faded == false)
        {
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
