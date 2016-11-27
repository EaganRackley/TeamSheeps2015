using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ElementScript : MonoBehaviour
{
    public bool isVisible;
    public List<Sprite> WhiteSprites;

    void Update()
    {
        Renderer render = GetComponent<Renderer>();
        if (render != null)
            render.enabled = isVisible;
    }

    public void SetVisible(bool visible)
    {
        isVisible = visible;
    }

    public void SetShapeFrame(int frame)
    {
        Debug.Assert(frame >= 0);
        Debug.Assert(frame < WhiteSprites.Count);
        GetComponent<SpriteRenderer>().sprite = WhiteSprites[frame];
    }

    /// <summary>
    /// Handles touch behavior triggered by the TouchAndMouseInput client script
    /// by calling the associated method on the parent player object (which will
    /// in turn change visibility for that object based on sever data)
    /// </summary>
    void OnTouchUp()
    {
        // If the local player 
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();

        foreach (var player in players)
        {
            if(player.isLocalPlayer)
            {
                player.SendMessage("OnHandleOnChildTouchUp", this);
            }
        }

        
    }
}

/* Interesting snippet on how to have on client update all other clients. We don't need it for this app.
private NetworkIdentity objNetId;

[ClientRpc]
void RpcChangeVisibility(bool visible)
{
    m_Visible = visible;
}

[Command]
void CmdSendDataToServer(bool visible)
{
    objNetId = GetComponent<NetworkIdentity>();        // get the object's network ID
    objNetId.AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
    RpcChangeVisibility(visible);
    objNetId.RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
}
*/


/*void OnTriggerEnter2D(Collider2D other)
{
    if(isClient)
    {
        print("Client entering trigger");
    }
    else if(isServer)
    {
        print("Server entering trigger");
        if (other.gameObject.tag == "Touch")
        {
            print("Server Changing Visibility on TouchDestroyer");
            m_Visible = !m_Visible;
        }
    }
}

void OnCollisionEnter2D(Collision2D col)
{
    if (isClient)
    {
        print("Client entering trigger");
    }
    else if (isServer)
    {
        print("Server entering trigger");
    }
}

    [SyncVar]
private bool m_Visible;

[ClientRpc]
void RpcChangeVisibility(bool visible)
{
    m_Visible = visible;
}

[Command]
void CmdChangeVisibilityOnServer(bool visible)
{
    myNetId = GetComponent<NetworkIdentity>();        // get the object's network ID
    myNetId.AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
    RpcChangeVisibility(visible);
    myNetId.RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
}

*/
