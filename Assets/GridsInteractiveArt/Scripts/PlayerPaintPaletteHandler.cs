using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPaintPaletteHandler : MonoBehaviour
{
    public List<Sprite> spriteShapes;
    public Color myPaletteColor = new Color(1.0f, 1.0f, 1.0f);
    public bool isSelected = false;
    public string ID;
    //private Vector3 myTargetScale = new Vector3(2.0f, 2.0f, 2.0f);
    private int myCurrentSprintShape;
    private float myTimer = 0f;

    // Use this for initialization
    void Start()
    {
        ID = this.gameObject.name;
        myCurrentSprintShape = 0;
        myTimer = 0f;
        HandleSelectionChanges();
    }

    private void UpdatePaletteShape()
    {
        if (!isSelected) return;
        myTimer += Time.deltaTime * 1.0f;
        if (myTimer > 2.0f)
        {
            myTimer = 0;
            GetComponent<SpriteRenderer>().sprite = spriteShapes[myCurrentSprintShape];
            HandleSelectionChanges();
            myCurrentSprintShape++;
            if (myCurrentSprintShape >= spriteShapes.Count)
            {
                myCurrentSprintShape = 0;
            }
        }
    }

    void Update()
    {
        float myTargetAlpha = 0.25f;
        if (isSelected)
        {
            myTargetAlpha = 1.0f;
        }

        UpdatePaletteShape();

        // Color currentColor = this.gameObject.GetComponent<Renderer>().material.color;
        // if(currentColor.a < myTargetAlpha)
        // {
        //     currentColor.a += 1.0f * Time.deltaTime;
        //     if (currentColor.a > myTargetAlpha) currentColor.a = myTargetAlpha;
        // }
        // else if (currentColor.a > myTargetAlpha)
        // {
        //     currentColor.a -= 1.0f * Time.deltaTime;
        //     if (currentColor.a < myTargetAlpha) currentColor.a = myTargetAlpha;
        // }

        //this.gameObject.GetComponent<Renderer>().material.color = currentColor;

        // if(isSelected)
        // {
        //     myTargetScale = new Vector3(3.0f, 3.0f, 3.0f);
        // }
        // else
        // {
        //     myTargetScale = new Vector3(2.0f, 2.0f, 2.0f);
        // }
        // 
        // Vector3 scale = this.gameObject.transform.localScale;
        // if (scale.x < myTargetScale.x) scale.x += 0.1f;
        // if (scale.y < myTargetScale.y) scale.y += 0.1f;
        // if (scale.x > myTargetScale.x) scale.x -= 0.1f;
        // if (scale.y > myTargetScale.y) scale.y -= 0.1f;
        // 
        // this.gameObject.transform.localScale = scale;
    }

    private void AssignColorToLocalPlayer()
    {
        // Find the local player element, and set the palette color for that player.
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();
        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                player.SendMessage("OnPaletteColorChanged", myPaletteColor);
            }
        }
    }

    private void DeselectOtherPaletteObjects()
    {
        PlayerPaintPaletteHandler[] paletteObjects = FindObjectsOfType<PlayerPaintPaletteHandler>();
        foreach (var paletteObject in paletteObjects)
        {
            if (paletteObject.ID != this.ID)
            {
                paletteObject.isSelected = false;
                paletteObject.HandleSelectionChanges();
            }
        }
    }

    private void ChangePaletteShape()
    {
        GetComponent<SpriteRenderer>().sprite = spriteShapes[myCurrentSprintShape];
        HandleSelectionChanges();
        myCurrentSprintShape++;
        if (myCurrentSprintShape >= spriteShapes.Count)
        {
            myCurrentSprintShape = 0;
        }
    }

    /// <summary>
    /// Gets selection sprite children and changes their selections to match the parent object.
    /// </summary>
    public void HandleSelectionChanges()
    {
        PaletteSelectionScript selectionSprite = GetComponentInChildren<PaletteSelectionScript>();
        selectionSprite.isSelected = isSelected;
        selectionSprite.myCurrentSprintShape = myCurrentSprintShape;
    }

    /// <summary>
    /// Handles touch behavior triggered by the TouchAndMouseInput client script
    /// by calling the associated method on the parent player object (which will
    /// in turn change visibility for that object based on sever data)
    /// </summary>
    void OnTouchUp()
    {
        // Change the shape of the palette object every time it's touched
        //ChangePaletteShape();

        // Indicate that the touched palette is now selected
        isSelected = true;

        // Find all the other palette elements, and deselect them.
        DeselectOtherPaletteObjects();

        // Assign the color of this palette object to the local player brush
        AssignColorToLocalPlayer();

        HandleSelectionChanges();
    }

}
