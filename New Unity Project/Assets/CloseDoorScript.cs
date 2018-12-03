using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorScript : MonoBehaviour {

    private Animator _anim;
    public GameObject colliderPrefab;
	void Start () {
        _anim = GetComponent<Animator>();
        _anim.SetFloat("speed", 0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 toPlayer = (other.transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward,toPlayer)>0)
            {
                CloseDoors();
            }
        }
    }

    private void CloseDoors()
    {
        Instantiate(colliderPrefab,transform.parent.position,transform.parent.rotation);
        _anim.SetFloat("speed", 1f);
    }
}
