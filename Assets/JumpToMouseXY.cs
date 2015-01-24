using UnityEngine;
using System.Collections;

public class JumpToMouseXY : MonoBehaviour 
{
	// Update is called once per frame
	void Update () {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
	}
}
