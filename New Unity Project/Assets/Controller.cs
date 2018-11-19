using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    public Camera c;

    public KeyCode Forward;
    public KeyCode Backward;
    public KeyCode Left;
    public KeyCode Right;
    public float ForwardSpeed = 1;
    public float BackwardSpeed = 0.5f;
    public float StrafeSpeed = 0.75f;
    public float StrafeCutoff = 0.2f;

    private Rigidbody _rb;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vDir = ViewingDirection();

        transform.forward = new Vector3(vDir.x, 0, vDir.z);
        Debug.DrawRay(transform.position, vDir, Color.blue);


        float speed;
        if (_rb.velocity == Vector3.zero)
        {
            {
                speed = ForwardSpeed;
                Debug.Log("Not Moving");
            }
        }
        else
        {
            Debug.DrawRay(transform.position, _rb.velocity, Color.red);
            vDir.y = 0;
            float d = Vector3.Dot(vDir.normalized, _rb.velocity.normalized);
            Debug.Log(d);
            if (d < StrafeCutoff && d > -StrafeCutoff)
            {
                speed = StrafeSpeed;
                Debug.Log("Strafing");
            }
            else if (d < -StrafeCutoff)
            {
                Debug.Log("Backwards");
                speed = BackwardSpeed;
            }
            else
            {
                Debug.Log("Forward");
                speed = ForwardSpeed;
            }
        }
        Vector3 v = Vector3.zero;

        if (Input.GetKey(Forward))
        {
            v += Vector3.forward;
        }
        if (Input.GetKey(Backward))
        {
            v += Vector3.back;
        }
        if (Input.GetKey(Left))
        {
            v += Vector3.left;
        }
        if (Input.GetKey(Right))
        {
            v += Vector3.right;
        }
        _rb.AddForce(v * speed, ForceMode.Acceleration);

    }

    Vector3 ViewingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray r = c.ScreenPointToRay(mousePos);
        RaycastHit info;
        if (Physics.Raycast(r, out info, 1000, 1 << 9))
        {
            return info.point - transform.position;
        }
        return -Vector3.one;
    }

}
