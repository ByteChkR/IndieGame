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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HUDScript.instance.GetNumberOfHelpedNPCs() > 2)
            {
                OpenDoor();
            }
        }
    }

    private void DeleteCollider()
    {
        Destroy(colliderObject);
    }
}
