﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("EaganTest2");
		}
	
	}
}
