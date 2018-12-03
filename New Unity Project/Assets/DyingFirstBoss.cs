using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingFirstBoss : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void DestroyBoss()
    {
        Destroy(gameObject);
        GameObject.Find("BossDoorOpenMihai").transform.GetChild(0).GetComponent<OpenBossDoor>().OpenDoor();
    }
}
