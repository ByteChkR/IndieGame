using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossTeleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AdditiveLevelManager.instance.RemoveLevel(1);
            AdditiveLevelManager.instance.LoadLevel(2);
        }
    }
}
