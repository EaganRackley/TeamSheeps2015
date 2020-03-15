using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTileInstantiator : MonoBehaviour {
    public GameObject FallingBlockPrefab;
    public GameObject TempGrassTile;

    public float top       = 45f;
    public float left      = -21f;
    public float bottom    = -5f;
    public float right     = 78f;
    public float increment = 10f; 

    private Vector2 currentPosition = Vector2.zero;
    private float waitTimer = 0f;
    private const float WAIT_PERIOD = 0.25f;
    private bool isActive = true;

    void Start ()
    {
        isActive = true;
        currentPosition = new Vector2(bottom, left);
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (isActive == false) return;
        // Every WAIT_PERIOD instantiate a new block until currentPosition has gone from BOTTOM/LEFT to TOP/RIGHT
        waitTimer += Time.deltaTime;
        if(waitTimer > WAIT_PERIOD)
        {
            waitTimer = 0.0f;
            Vector3 spawnPosition = this.transform.position;
            spawnPosition.x = currentPosition.x;
            spawnPosition.y = currentPosition.y;
            spawnPosition.z += 0.0001f;
            var newObject = GameObject.Instantiate(FallingBlockPrefab, spawnPosition, Quaternion.identity);
            //newObject.transform.parent = this.gameObject.transform;
            currentPosition.x += increment;
            if (currentPosition.x > right)
            {
                currentPosition.x = left;
                currentPosition.y += increment;
                if(currentPosition.y > top)
                {
                    isActive = false;
                    //Destroy(TempGrassTile);
                }
            }
            
        }
    }
}
