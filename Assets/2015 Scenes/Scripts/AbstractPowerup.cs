using UnityEngine;
using System.Collections;

public abstract class AbstractPowerup : MonoBehaviour
{
    public abstract IEnumerator PowerupFunction(PlayerController player);

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2")
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            player.GetPowerup(this.PowerupFunction);
            Destroy(this.gameObject);
        }
    }
}
