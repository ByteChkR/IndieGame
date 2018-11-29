using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewLock : MonoBehaviour
{

    public Transform Target;
    Vector3 _distance;
    public static Camera Cam;
    // Use this for initialization
    void Start()
    {
        _distance = transform.position - Target.position;
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target != null) transform.position = Target.position + _distance;
    }
}
