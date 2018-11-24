using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossTrioBullet : MonoBehaviour {

    public float speed = 5;
    private Rigidbody _rb;
	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        _rb.velocity = transform.forward * speed;
	}
}
