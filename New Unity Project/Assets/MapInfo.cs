﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapMetaData
{
    public string Name;
    public bool isTurorial;
    public bool isMenu;

}

public class MapInfo : MonoBehaviour {


    public MapMetaData data;
    public Controller controller;
    public GameObject EnemyContainer;
    public GameObject bulletContainer;
    public static GameObject BulletContainer;

    // Use this for initialization
    void Start () {
        BulletContainer = bulletContainer;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        if (!data.isTurorial)
        {
            //controller.EnableDashing = controller.EnableMovement = controller.EnableShooting = true;
        }
	}

}
