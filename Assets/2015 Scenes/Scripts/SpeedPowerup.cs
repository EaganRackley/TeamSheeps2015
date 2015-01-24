using UnityEngine;
using System.Collections;

public class SpeedPowerup : AbstractPowerup
{
    public float speedIncrease = 500f;
    public float powerupDuration = 5f;

    public override IEnumerator PowerupFunction(PlayerController player)
    {
        player.speed += speedIncrease;
        yield return new WaitForSeconds(powerupDuration);
        player.speed -= speedIncrease;
    }
}
