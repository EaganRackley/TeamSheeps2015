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
    /// <summary>
    ///  Single instance of the server object (for some reason not using this idiom causes issues with more than one item trying to be the server - I don't know what I'm doing wrong, but this works)
    /// </summary>
    public static GridPlayerScript myMasterInstance;

    /// <summary>
    /// Indicates whether the player object is the master instance that stores the ServerElements property used by other players.
    /// </summary>
    [SyncVar]
    public bool isMasterInstance = false;

    /// <summary>
    /// SyncList object of ElementProperty values
    /// </summary>
    public class SyncListProperties : SyncListStruct<ElementProperty> { }

    /// <summary>
    /// These are the elements that remain synced between the server and the client.
    /// </summary>
    public SyncListProperties ServerElements = new SyncListProperties();

    private Color myClientColor;

    /// <summary>
    /// Paint elements based on game objects in the scene that we can set properties on based on the server sync elements.
    /// </summary>
    private Dictionary<string, GameObject> mySceneElements = new Dictionary<string, GameObject>();   

    /// <summary>
    /// Indicates whether the client frame is dirty or not, and needs to be updated based on changes in the server elements
    /// </summary>
    private bool myFrameIsDirty = false;    

    /// <summary>
    /// GameObject start method sets reference to the single instance on the server.
    /// </summary>
    void Start()
    {
        //myMasterInstance = this;           
        myClientColor = new Color(1.0f, 0.0f, 0.1f);
    }

    void setClientClolor(Color clientColor)
    {
        myClientColor = clientColor;
    }

    /// <summary>
    /// When the server starts populate ServerElements with all the default properties of the GameObjects in the scene with an "ElementScript" attribute.
    /// (This way our server elements are always updated dynamically based on what's actually in the scene)
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();

        // Get the master player script object on the server, if one doesn't
        // exist already, then assign this player object as the master instance
        // so that other objects will use it as such.
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();
        if (players.Length <= 1)
        {
            isMasterInstance = true;
            myMasterInstance = this;
            myMasterInstance.ServerElements.Clear();

            ElementScript[] elements = FindObjectsOfType<ElementScript>();

            foreach (var element in elements)
            {
                ElementProperty newProperty = new ElementProperty(element.gameObject.name, element.gameObject.GetComponent<ElementScript>().isVisible, Random.Range(0, 12), isServer);
                myMasterInstance.ServerElements.Add(newProperty);
            }
        }
        else
        {
            foreach (GridPlayerScript player in players)
            {
                if (player.isMasterInstance == true)
                {
                    myMasterInstance = player;
                }
            }
        }        
        
        // Use this to print debug messages when server elements change
        // myServerElements.Callback = OnServerElementsChanged;
    }

    /// <summary>
    /// When the client starts we populate mySceneElemetns with all the GameObjects in the scene with an "ElementScript" attribute.
    /// (This way we can always assign properties in ServerElements visually to the scene when there's an update)
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();

        // Get the master player script object on the server, if one doesn't
        // exist already, then assign this player object as the master instance
        // so that other objects will use it as such.
        GridPlayerScript[] players = FindObjectsOfType<GridPlayerScript>();
        if (players.Length <= 1)
        {
            myMasterInstance = this;
        }
        else
        {
            foreach (GridPlayerScript player in players)
            {
                if (player.isMasterInstance == true)
                {
                    myMasterInstance = player;
                }
            }
        }

        // Set up references to the paint elements that we'll be modifying from the scene
        ElementScript[] paintElements = FindObjectsOfType<ElementScript>();
        foreach (var element in paintElements)
        {
            mySceneElements.Add(element.gameObject.name, element.gameObject);
        }
        
        myFrameIsDirty = true;
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
            foreach (ElementProperty element in myMasterInstance.ServerElements)
            {
                if (mySceneElements.ContainsKey(element.ID))
                {
                    //mySceneElements[element.ID].GetComponent<ElementScript>().SetVisible(element.isVisible);
                    mySceneElements[element.ID].GetComponent<ElementScript>().SetShapeFrame(element.shapeFrame);
                    mySceneElements[element.ID].GetComponent<Renderer>().material.color = element.shapeColor;

                    if(element.ID == "ElementR0C0")
                    {
                        Debug.Log("Element R0C0 Visibility set to: " + element.isVisible.ToString());
                    }
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
        CmdServerSetVisibility(element.name, !element.isVisible, myClientColor);
    }


    public void OnConnectedToServer()
    {
        
    }

    /// <summary>
    ///  Server method called by OnHandleChildTouchUp that will call the set visibility command on the single sever object.
    /// </summary>
    /// <param name="ID">The name of the component to change.</param>
    /// <param name="isVisible">Whether to show or hide that component.</param>
    [Command]
    public void CmdServerSetVisibility(string ID, bool isVisible, Color color)
    {
        myMasterInstance.CmdSetVisibility(ID, isVisible, color);
    }

    /// <summary>
    ///  This Command method sets the network sync properties for all clients based on the parameters specified.
    ///  Elements have to be removed and inserted in order to get the struct to sync, it seems to be a bug in unity
    ///  that hasn't been addressed yet.
    /// </summary>
    /// <param name="ID">The ID of the shape being changed.</param>
    /// <param name="isVisible">The visible state of the shape being changed.</param>
    [Command]
    public void CmdSetVisibility(string ID, bool isVisible, Color color)
    {
        Debug.Log("CmdSetVisibility ID: " + ID + " Visible: " + isVisible + " Server: " + isServer);        
        int targetIndex = -1;
        ElementProperty newElement = new ElementProperty("Empty", false, 0, isServer);
        for (int index = 0; index < ServerElements.Count; index++)
        {
            ElementProperty element = ServerElements[index];
            if (element.ID.Equals(ID))
            {
                targetIndex = index;
                newElement.ID = element.ID;
                newElement.isServer = element.isServer;
                newElement.isVisible = element.isVisible;
                newElement.shapeColor = color;
                if (newElement.isVisible == false && isVisible == true)
                {
                    newElement.shapeFrame = Random.Range(0, 12);
                }
                newElement.isVisible = isVisible;
                ServerElements[targetIndex] = newElement;
                RpcClientFrameIsDirty(newElement.ID, newElement.isVisible, newElement.shapeColor);
            }
        }        
    }

    /// <summary>
    /// Sets the client frame to dirty when server 
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="isVisible"></param>
    [ClientRpc]
    void RpcClientFrameIsDirty(string ID, bool isVisible, Color color)
    {           
        myFrameIsDirty = true;
        if(mySceneElements.ContainsKey(ID))
        {
            mySceneElements[ID].GetComponent<ElementScript>().SetVisible(isVisible);            
        }
        
    }

    /// <summary>
    /// Debug routine triggered every time a server element is changed.
    /// </summary>
    //void OnServerElementsChanged(SyncListStruct<ElementProperty>.Operation op, int itemIndex)
    //{
    //    string output = "ElementsChanged Server: " + myServerElements[itemIndex].isServer;
    //
    //    for (int index = 0; index < mySyncElementProperties.Count; index++)
    //    {
    //        output += "," + mySyncElementProperties[index].isVisible.ToString();
    //    }
    //    
    //     Debug.Log(output);
    //
    //     if (op == SyncListStruct<ElementProperty>.Operation.OP_INSERT)
    //     {
    //         myFrameIsDirty = true;
    //     }
    //}



}
