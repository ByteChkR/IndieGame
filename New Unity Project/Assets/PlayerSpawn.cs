﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {


	// Use this for initialization
	void Start () {
		if(Unit.Player != null)
        {
            Unit.Player.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Unit.Player.ToggleUnitMovement(true);
        }
        else
        {
            Debug.Log("Player Var is null.");
        }
        
	}
	
}
