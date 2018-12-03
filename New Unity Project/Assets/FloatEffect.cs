using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour {

    public float range = 0.3f;
    public float speed = 1;
    private float _initialY;
    private float _curentTime;
	void Start () {
        _initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        _curentTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, _initialY + Mathf.Sin(_curentTime*speed)*range, transform.position.z);
	}
}
