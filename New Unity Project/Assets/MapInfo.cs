using System.Collections;
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

    // Use this for initialization
    void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        if (!data.isTurorial)
        {
            //controller.EnableMovement = true;
        }
	}

}
