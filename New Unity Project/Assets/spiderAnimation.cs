using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAnimation : MonoBehaviour {

    private Animator _anim;
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_anim.GetInteger("state"));
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _anim.SetInteger("state", 0);
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _anim.SetInteger("state", 1); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _anim.SetInteger("state", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _anim.SetInteger("state", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _anim.SetInteger("state", 4);
        }

    }
}
