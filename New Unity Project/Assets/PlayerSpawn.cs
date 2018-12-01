﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    bool a = true;
	// Use this for initialization
	void Start () {

        CameraController.instance.Load("CameraEnter");
        CameraViewLock.instance.start = false;
        if (Unit.Player != null)
        {
            Unit.Player.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Unit.Player.ToggleUnitMovement(true);
        }
        else
        {
            Debug.Log("Player Var is null.");
        }
    }

    private void FixedUpdate()
    {
        if (a && !CameraController.instance.start)
        {

            
            
            CameraViewLock.instance.start = true;
            
            a = false;
        }
    }

}
