﻿using UnityEngine;
using System.Collections;

public class ReturnToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E))
        {
            Application.LoadLevel(6);
        }

	}
}
