using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_rotate_script : MonoBehaviour {

    public Vector3 speed;

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(speed * Time.deltaTime);

		

    }
}
