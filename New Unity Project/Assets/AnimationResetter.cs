using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResetter : MonoBehaviour {

    Animator _anim;
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void ResetAnimation()
    {
        _anim.SetInteger("state", 0);
    }
}
