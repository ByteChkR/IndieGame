using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManTest : MonoBehaviour {

    private Animator _anim;
    private float _clickTimer = 0;
	void Start () {
        _anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _anim.SetInteger("state", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _anim.SetInteger("state", 1);
        }*/
        if(Input.GetMouseButtonDown(0))
        {
            _anim.SetInteger("state", 1);
            _clickTimer = 0.5f;
        }

        if(_clickTimer>=0)
        {
            _clickTimer -= Time.deltaTime;
        }
    }

    private void CheckForGoingToIdle()
    {
        if(_clickTimer <=0)
        {
            _anim.SetInteger("state", 0);
        }
    }

    private void BackToIdle()
    {
        _anim.SetInteger("state", 0);
    }
}
