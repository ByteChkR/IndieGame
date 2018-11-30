using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpDistance : MonoBehaviour {

    public float aproximatedTime;
    public Vector3 offSet;
    private Vector3 destination;
    [Range(0.0f, 1.0f)]
    public float lerppower = 0.1f;
	void Start () {
        destination = transform.position + offSet;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, destination,lerppower);
        aproximatedTime -= Time.deltaTime;
        if(aproximatedTime <0)
        {
            Destroy(this);
        }
    }
}
