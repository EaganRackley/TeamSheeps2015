using UnityEngine;
using System.Collections;

public abstract class AbstractPowerup : MonoBehaviour
{
    public abstract IEnumerator PowerupFunction(PlayerController player);

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            player.GetPowerup(this.PowerupFunction);
            Destroy(this.gameObject);
        }
    }
}
