using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawn : MonoBehaviour {

    public bool FollowPlayerOnThisMap = true;
    // Use this for initialization
    void Start () {
        CameraViewLock.instance.FollowPlayer = FollowPlayerOnThisMap;
        CameraViewLock.instance.Cam.transform.SetPositionAndRotation(transform.position, transform.rotation);
	}
	
}
