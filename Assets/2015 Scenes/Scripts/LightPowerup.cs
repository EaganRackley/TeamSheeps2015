using UnityEngine;
using System.Collections;

public class LightPowerup : AbstractPowerup {

    public float radiusIncrease;
    public float intensityIncrease;
    public float duration;

    public override IEnumerator PowerupFunction(PlayerController player)
    {
        player.playerLight.range += radiusIncrease;
        player.playerLight.intensity += intensityIncrease;
        yield return new WaitForSeconds(duration);
        player.playerLight.range -= radiusIncrease;
        player.playerLight.intensity -= intensityIncrease;
    }

}
