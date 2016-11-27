﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/// <summary>
///  This is the master script for the GridPlayerObject that will control each individual painted element
///  on the grid board, including position, visibility, and color.
/// </summary>
public class GridPlayerScript : NetworkBehaviour
{   
    public class SyncListProperties : SyncListStruct<ElementProperty> { }

    public GameObject paintElementPrefab;

    private Dictionary<string, GameObject> myPaintElements = new Dictionary<string, GameObject>();

    public SyncListProperties mySyncElementProperties = new SyncListProperties();

    public List<ElementProperty> myElementsProperties = new List<ElementProperty>();

    public bool myFrameIsDirty = false;

    
    /// <summary>
    /// When the server starts we populate our SyncList myElements with a list of properties associated with the default state of each
    /// child element in the piece. From this point forward that list will dictate the master state applied to each child object across
    /// all network players.
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();
       
        mySyncElementProperties.Clear();

        ElementScript[] elements = FindObjectsOfType<ElementScript>();

        foreach (var element in elements)
        {
            ElementProperty newProperty = new ElementProperty(element.gameObject.name, element.gameObject.GetComponent<ElementScript>().isVisible, Random.Range(0, 12), isServer);
            mySyncElementProperties.Add(newProperty);
        }

        mySyncElementProperties.Callback = OnServerElementsChanged;
    }

    /// <summary>
    /// On Start the player objects obtains a list of all the paint elements in the scene to reference later...
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();
        ElementScript[] paintElements = FindObjectsOfType<ElementScript>();
        foreach (var element in paintElements)
        {
            myPaintElements.Add(element.gameObject.name, element.gameObject);
        }

        myFrameIsDirty = true;
    }
    
    void OnServerElementsChanged(SyncListStruct<ElementProperty>.Operation op, int itemIndex)
    {
        string output = "ElementsChanged Server: " + mySyncElementProperties[itemIndex].isServer;

        //for (int index = 0; index < mySyncElementProperties.Count; index++)
        //{
        //    output += "," + mySyncElementProperties[index].isVisible.ToString();
        //}
        //
        // Debug.Log(output);

        // if (op == SyncListStruct<ElementProperty>.Operation.OP_INSERT)
        // {
        //     myFrameIsDirty = true;
        // }
    }

    /// <summary>
    /// The update method for the player object iterates through all child objects, locates the object associated with the element ID
    /// (e.g. object name) and modifies the properties of that object by what's on the server.
    /// </summary>
    void Update()
    {
        if(myFrameIsDirty)
        {
            myFrameIsDirty = false;
            foreach (ElementProperty element in myElementsProperties)
            {
                if (myPaintElements.ContainsKey(element.ID))
                {
                    myPaintElements[element.ID].GetComponent<ElementScript>().isVisible = element.isVisible;
                    myPaintElements[element.ID].GetComponent<ElementScript>().SetShapeFrame(element.shapeFrame);
                }
            }
        }
    }

    /// <summary>
    ///  Method called by child objects to handle input behavior, will triggering property changes
    ///  based on current player state
    /// </summary>
    /// <param name="elementID"></param>
    public void OnHandleOnChildTouchUp(ElementScript element)
    {
        CmdSetVisibility(element.name, !element.isVisible);
    }


    /// <summary>
    ///  This Command method sets the network sync properties for all clients based on the parameters specified.
    ///  Elements have to be removed and inserted in order to get the struct to sync, it seems to be a bug in unity
    ///  that hasn't been addressed yet.
    /// </summary>
    /// <param name="ID">The ID of the shape being changed.</param>
    /// <param name="isVisible">The visible state of the shape being changed.</param>
    [Command]
    public void CmdSetVisibility(string ID, bool isVisible)
    {
        Debug.Log("CmdSetVisibility ID: " + ID + " Visible: " + isVisible + " Server: " + isServer);

        int targetIndex = -1;
        ElementProperty newElement = new ElementProperty("Empty", false, 0, isServer);
        for (int index = 0; index < mySyncElementProperties.Count; index++)
        {
            ElementProperty element = mySyncElementProperties[index];
            if (element.ID.Equals(ID))
            {
                targetIndex = index;
                newElement = element;
            }
        }
        if (targetIndex >= 0)
        {
            if (newElement.isVisible == false && isVisible == true)
            {
                newElement.shapeFrame = Random.Range(0, 12);
            }
            newElement.isVisible = isVisible;
            mySyncElementProperties.RemoveAt(targetIndex);
            mySyncElementProperties.Insert(targetIndex, newElement);
            //myFrameIsDirty = true;
        }
        RpcSetVisibility(ID, isVisible);
    }

    [ClientRpc]
    void RpcSetVisibility(string ID, bool isVisible)
    {
        
        myElementsProperties.Clear();
        foreach(ElementProperty element in mySyncElementProperties)
        {
            myElementsProperties.Add(element);
        }

        myFrameIsDirty = true;
    }


}
