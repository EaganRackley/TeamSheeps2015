using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPaintPaletteHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        NetworkBehaviour parentNB = GetComponentInParent<NetworkBehaviour>();
        if(parentNB != null)
        {
            if(!parentNB.isLocalPlayer)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
