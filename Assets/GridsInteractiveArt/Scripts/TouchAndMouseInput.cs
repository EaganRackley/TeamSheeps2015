using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TouchAndMouseInput : NetworkBehaviour {
    public LayerMask touchInputMask;
    public Camera inputCamera;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] oldTouchList;
    private RaycastHit hit;

    // Update is called once per frame
    void Update ()
    {
        if (inputCamera == null) return;

        // If there's only a single touch or a user is releasing the mouse button
        // use mouse input controls to interact with the scene. Otherwise use touch
        // input controls.
        if(Input.touchCount == 1 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            oldTouchList = new GameObject[touchList.Count];
            touchList.CopyTo(oldTouchList);
            touchList.Clear();

            //Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = inputCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, touchInputMask))
            {
                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);
                TouchPhase phase = TouchPhase.Canceled;

                if (Input.GetMouseButtonDown(0))
                {
                    phase = TouchPhase.Began;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    phase = TouchPhase.Ended;
                }
                else if (Input.touchCount > 0)
                {
                    phase = Input.GetTouch(0).phase;
                }

                if( phase == TouchPhase.Began )
                {
                    //print("OntouchDown");
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                else if (phase == TouchPhase.Ended)
                {
                    //   print("OntouchUp");
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }
                else if (phase == TouchPhase.Canceled)
                {
                    //print("OntouchExit");
                    recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
            // Determine which was touched before and aren't touched in our new list, and 
            // trigger an exit signal meaning that the user was still pressing when they lost focus
            // on the object.
            foreach(GameObject go in oldTouchList)
            {
                if(!touchList.Contains(go))
                {
                    //print("OntouchExit");
                    go.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else if(Input.touchCount >= 2)
        {
            //foreach(Touch touch in Input.touches)
            // TODO: Here we'll support TouchPhase.Moved for swipe and pinch operations
            //       evaluating if there are two touches, and whether they have moved closer
            //       or further apart and then adjusting the camera zoom within bounds accordingly
            
        }

    }
}