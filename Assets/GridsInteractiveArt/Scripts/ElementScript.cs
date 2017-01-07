using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ElementScript : MonoBehaviour
{
    public bool isVisible;
    public List<Sprite> spriteShapes;
    public GameObject myPaintBullet;

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
        Debug.Assert(frame < spriteShapes.Count);
        GetComponent<SpriteRenderer>().sprite = spriteShapes[frame];
    }

    private void TouchSpecialEffect()
    {
        // Create a paint bullet effect to distract the user in case of a slow network
        GameObject bullet = Instantiate(myPaintBullet, Vector3.zero, Quaternion.identity) as GameObject;
        bullet.transform.position = this.gameObject.transform.position;
    }

    /// <summary>
    /// Handles touch behavior triggered by the TouchAndMouseInput client script
    /// by calling the associated method on the parent player object (which will
    /// in turn change visibility for that object based on sever data)
    /// </summary>
    void OnTouchUp()
    {
        // If the local player then send the player a touch message for this object
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();

        foreach (var player in players)
        {
            if(player.isLocalPlayer)
            {
                player.SendMessage("OnHandleOnChildTouchUp", this);
            }
        }

        TouchSpecialEffect();
    }

    void OnTouchMoved()
    {
        // If the local player 
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();

        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                player.SendMessage("OnHandleOnChildTouchUp", this);
            }
        }

        TouchSpecialEffect();
    }
}
