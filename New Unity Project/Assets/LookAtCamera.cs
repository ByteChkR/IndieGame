using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

private Camera _cam;
	void Start () {
        _cam = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {
       transform.LookAt(_cam.transform.position);
        transform.localRotation *= transform.parent.rotation;
	}
}
