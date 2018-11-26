using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public float TimeLeft;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TimeLeft -= Time.deltaTime;
        if(TimeLeft<0)
        {
            Destroy(gameObject);
        }
	}
}
