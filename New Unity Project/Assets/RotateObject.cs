using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public Vector3 rotateVector;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotateVector);
	}
}
