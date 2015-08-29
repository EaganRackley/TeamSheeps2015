using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncInterpolater : NetworkBehaviour
{

    [Header("Options")]
    public float smoothSpeed = 10f;

    [SyncVar]
    private Vector3 mostRecentPos;
    [SyncVar]
    private Quaternion mostRecentRotation;

    private Vector3 prevPos;

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            // If moved, send my data to server
            if (prevPos != transform.position)
            {
                // Send position to server (function runs server-side)
                CmdSendDataToServer(transform.position, transform.rotation);

                prevPos = transform.position;
            }
        }
        else
        {
            // Apply position to other players (mostRecentPos read from Server vis SyncVar)
            this.transform.position = Vector3.Lerp(transform.position, mostRecentPos, smoothSpeed * Time.deltaTime);
            this.transform.rotation = mostRecentRotation;
        }
    }

    [Command]
    void CmdSendDataToServer(Vector3 pos, Quaternion rotation)
    {
        mostRecentPos = pos;
        mostRecentRotation = rotation;
    }

}