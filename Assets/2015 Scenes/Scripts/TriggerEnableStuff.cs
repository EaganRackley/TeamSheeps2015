using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnableStuff : MonoBehaviour
{
    public GameObject[] StuffToEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CameraFollow")
        {
            foreach(GameObject obj in StuffToEnable)
            {
                if (obj.tag != "Player" && obj.tag != "Player2")
                    obj.SetActive(true);
                else
                    obj.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
