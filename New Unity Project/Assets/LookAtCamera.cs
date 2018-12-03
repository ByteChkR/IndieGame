using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

private Camera _cam;
	void Start () {
        _cam = Camera.main;
        transform.forward = -_cam.transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
