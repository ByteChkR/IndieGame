using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseLastBoss : MonoBehaviour {

    public GameObject bossPack;
    public float range = 16;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Unit.Player == null)
        {
            return;
        }

        if (Vector3.Distance(Unit.Player.transform.position, transform.position)<range)
            {
            Instantiate(bossPack, transform.position, transform.rotation);
            Destroy(gameObject);
        }
		
	}
}
