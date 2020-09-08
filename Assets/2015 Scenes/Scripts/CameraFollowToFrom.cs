using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowToFrom : MonoBehaviour
{
    public Transform From;
    public Transform Target;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = From.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Target.transform.position, Time.deltaTime * Speed);
    }
}
