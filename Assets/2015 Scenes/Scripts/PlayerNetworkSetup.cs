using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// This script handles configuring each NetworkPlayer object when it's first spawned by the Network Manager.
/// </summary>
public class PlayerNetworkSetup : NetworkBehaviour {
	public RuntimeAnimatorController lightHairMale;
	public RuntimeAnimatorController lightHairFemale;
	public RuntimeAnimatorController darkHairMale;
	public RuntimeAnimatorController darkHairFemale;

	// Use this for initialization
	void Start () 
	{
        // Grab the Network Players Text Mesh component so we can set the player name
		TextMesh nameLabel = GetComponentsInChildren<TextMesh>()[0];

		// If we're setting up the local player, we want the camera to follow him/her so we set the "Player" tag
		// (all other players are tagged "OtherPlayer" by default, and enable the PlayerController object so that
		// it's represented over the network : - )
		if (isLocalPlayer) 
		{
			GetComponent<PlayerController>().enabled = true;
			this.tag = "Player";
			nameLabel.text = "Player id: local";
		}
		else
		{
			nameLabel.text = "Player id: " + netId.ToString();
		}

        // Based on the player ID, set the character to be used by the player.
        int playerType = int.Parse(netId.ToString()) % 4;

        if (playerType == 0) 
		{
			GetComponent<Animator>().runtimeAnimatorController = lightHairMale;
		}
        else if (playerType == 1) 
		{
			GetComponent<Animator>().runtimeAnimatorController = lightHairFemale;
		}
        else if (playerType == 2) 
		{
			GetComponent<Animator>().runtimeAnimatorController = darkHairMale;
		}
        else if (playerType == 3) 
		{
			GetComponent<Animator>().runtimeAnimatorController = darkHairFemale;
		}
	}

}
