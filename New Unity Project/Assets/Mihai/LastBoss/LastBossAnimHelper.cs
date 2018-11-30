using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossAnimHelper : MonoBehaviour {

    public bool stop = true;
    
	void Start () {
        GameObject.Find("FinalBoss").GetComponent<Animator>().SetBool("isIdle", stop);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
