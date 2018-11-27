using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeforeDestroy : MonoBehaviour {

    public GameObject prefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create()
    {
        if (prefab != null)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
