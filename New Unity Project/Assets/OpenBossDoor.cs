using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour {

    public GameObject colliderObject;
    public Animator _anim;
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDoor()
    {
        _anim.SetFloat("speed", 1);
    }

    private void DeleteCollider()
    {
        Destroy(colliderObject);
    }
}
