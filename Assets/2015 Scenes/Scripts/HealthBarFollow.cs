using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform FollowTransform;
    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        this.transform.position = FollowTransform.position - Offset;
    }
}
