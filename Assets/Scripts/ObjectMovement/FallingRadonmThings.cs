﻿using UnityEngine;
using System.Collections;

public class FallingRadonmThings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" )
		{
			BroadcastMessage("SetRandomGravity");	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
