using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorHelper : MonoBehaviour {

    public float timeLeft =7;
    public GameObject prefab;
    public Transform firePlaceTransform;
    public Vector3 positionOffset;
    private bool _readyToStart = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(_readyToStart == false)
        {
            return;
        }


        if(prefab == null || firePlaceTransform == null )
        {
            Debug.Log("no prefab or fire place");
            Destroy(gameObject);
            return;
        }


        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            Instantiate(prefab,firePlaceTransform.position+positionOffset, firePlaceTransform.rotation);
            Destroy(gameObject);
        }
	}

    public void SetReadyToStart()
    {
        _readyToStart = true;
    }
}
