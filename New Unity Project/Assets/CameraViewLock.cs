using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewLock : MonoBehaviour
{
    public static CameraViewLock instance;
    public bool FollowPlayer = true;
    public Transform Target;
    Vector3 _distance;
    public Camera Cam;
    // Use this for initialization
    private void Awake()
    {
        if (instance != null) Debug.LogError("CameraViewLockNeeds to be singleton. Make sure there is only one instance.");
        instance = this;
    }
    void Start()
    {
        if (Target == null)
        {
            Target = Unit.Player.transform;
        }
        if(Target != null)_distance = transform.position - Target.position;
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target != null && FollowPlayer) transform.position = Target.position + _distance;
    }
}
