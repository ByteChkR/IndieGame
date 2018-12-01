﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewLock : MonoBehaviour
{
    public static CameraViewLock instance;
    public bool FollowPlayer = true;
    public Transform Target;
    Vector3 _direction;
    public Camera Cam;
    public float MinDistanceToPlayer = 17f;
    public float AddDistanceOnSpeed = 2f;
    public int SpeedAverageCount = 10;
    public Queue<Vector3> lastSpeeds;
    public float MaxSpeed;
    public bool start = false;
    public bool OnlyLerp = false;
    // Use this for initialization
    private void Awake()
    {
        if (instance != null) Debug.LogError("CameraViewLockNeeds to be singleton. Make sure there is only one instance.");
        instance = this;
        lastSpeeds = new Queue<Vector3>();
    }
    void Start()
    {
        if (Target == null)
        {
            Target = Unit.Player.transform;
        }
        if (Target != null)
        {
            _direction = (transform.position - Target.position);
            //MinDistanceToPlayer = _direction.magnitude;
            _direction.Normalize();
        }

        Cam = GetComponent<Camera>();
    }

    public void Reset()
    {

        Cam.transform.position = Target.transform.position + MinDistanceToPlayer * _direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null && FollowPlayer && start)
        {
            lastSpeeds.Enqueue(Unit.Player.rb.velocity);
            if (lastSpeeds.Count > SpeedAverageCount) lastSpeeds.Dequeue();
            Vector3 speed = OnlyLerp ? Unit.Player.rb.velocity : GetAvgSpeed();

            if (speed == Vector3.zero && !OnlyLerp)
            {
                transform.position = Target.position + _direction * MinDistanceToPlayer;
            }
            else
            {
                if (OnlyLerp)
                {
                    transform.position = Vector3.Lerp(transform.position, Target.position + _direction * MinDistanceToPlayer, 0.2f);
                }
                else
                {

                    transform.position = Vector3.Lerp(
                        transform.position,
                        Target.position + _direction * MinDistanceToPlayer + speed.normalized * Mathf.Clamp01(speed.magnitude / MaxSpeed) * AddDistanceOnSpeed,
                        Mathf.Clamp01(speed.magnitude / MaxSpeed));
                }

            }
        }
    }

    Vector3 GetAvgSpeed()
    {
        Vector3 speed = Vector3.zero;
        foreach (Vector3 s in lastSpeeds)
        {
            speed += s;
        }
        return speed / lastSpeeds.Count;
    }
}
