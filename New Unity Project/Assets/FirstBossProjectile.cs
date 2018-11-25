using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossProjectile : MonoBehaviour {

    public float speed;
    public float damage;
    public float distanceoffset;
    private Rigidbody _rb;
	void Start () {
        transform.position += transform.forward * distanceoffset;
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        _rb.velocity = transform.forward * speed;
	}
}
