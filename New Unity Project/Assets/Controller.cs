using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IController
{

    Vector3 VTarget { get; }
}

[RequireComponent(typeof(Rigidbody), typeof(Unit))]
public class Controller : MonoBehaviour, IController
{
    public Camera c;
    Vector3 target;
    public Vector3 VTarget { get { return target; } }

    public KeyCode Forward;
    public KeyCode Backward;
    public KeyCode Left;
    public KeyCode Right;
    public float ForwardSpeed = 1;
    public float BackwardSpeed = 0.5f;
    public float StrafeSpeed = 0.75f;
    public float StrafeCutoff = 0.2f;

    private Rigidbody _rb;
    Unit u;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        u = GetComponent<Unit>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vDir = ViewingDirection();
        vDir = new Vector3(vDir.x, 0, vDir.z);
        target = transform.position + vDir;
        vDir.Normalize();
        u.vDirNorm = vDir;

        Debug.DrawRay(transform.position, vDir * 5, Color.blue);

        transform.forward = vDir;

        float speed;
        if (_rb.velocity == Vector3.zero)
        {
            {
                speed = ForwardSpeed;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, _rb.velocity, Color.red);
            float d = Vector3.Dot(vDir, _rb.velocity.normalized);
            if (d < StrafeCutoff && d > -StrafeCutoff)
            {
                speed = StrafeSpeed;
            }
            else if (d < -StrafeCutoff)
            {
                speed = BackwardSpeed;
            }
            else
            {
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
        _rb.AddForce(v.normalized * speed, ForceMode.Acceleration);

    }

    Vector3 ViewingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray r = c.ScreenPointToRay(mousePos);
        RaycastHit info;
        if (Physics.Raycast(r, out info, 1000, 1 << 9))
        {
            return info.point - transform.position; //Position only because the camera is not a child object 
        }
        return -Vector3.one;
    }

}
