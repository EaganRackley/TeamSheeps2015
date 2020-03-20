using UnityEngine;
using System.Collections;

public class HealthPowerup : AbstractPowerup
{
    public float HealthIncrease;
    
    public override IEnumerator PowerupFunction(PlayerController player)
    {
        while(HealthIncrease > 0f)
        {
            player.speed += Time.deltaTime;
            HealthIncrease -= Time.deltaTime;
            if (!player.HealingParticles().isPlaying)
                player.HealingParticles().Play();
            yield return new WaitForSeconds(0.16f);
        }
        if (player.HealingParticles().isPlaying)
            player.HealingParticles().Stop();

    }
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player2")
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            player.GetPowerup(this.PowerupFunction);
            Destroy(this.gameObject);
        }
    }

}
