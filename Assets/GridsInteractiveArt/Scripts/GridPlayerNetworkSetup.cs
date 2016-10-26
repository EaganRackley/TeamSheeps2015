using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/// <summary>
/// This script handles configuring each NetworkPlayer object when it's first spawned by the Network Manager.
/// </summary>
public class GridPlayerNetworkSetup : NetworkBehaviour {

    // Use this for initialization
    void Start () 
    {
        // If we're setting up the local player, we want to enable their touch input controls
        // otherwise we won't do that...
        if (isLocalPlayer) 
        {
            this.tag = "Player";
            GetComponent<TouchAndMouseInput>().enabled = true;
            Camera camera = GameObject.FindObjectOfType<Camera>();
            GetComponent<TouchAndMouseInput>().inputCamera = camera;
            Vector3 pos = this.transform.position;
            pos.z = -2;
            this.transform.position = pos;
        }
        else
        {
            GetComponent<TouchAndMouseInput>().enabled = false;
            // Destory all the child objects since this isn't actually our local player...
            // (otherwise they will overlap on the grid and cause problems)
            //var children = new List<GameObject>();
            //foreach (Transform child in transform) children.Add(child.gameObject);
            //children.ForEach(child => Destroy(child));
        
        }
    }


}
