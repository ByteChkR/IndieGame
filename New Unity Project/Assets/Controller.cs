using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Unit))]
public class Controller : MonoBehaviour, IController
{
    public Camera Camera;
    public Animator Animator;
    Vector3 _target;
    public bool MakeControlsHardlyRetarded = true;
    public Vector3 VTarget { get { return _target; } }
    bool _lockControls = false;
    public KeyCode Forward;
    public KeyCode Backward;
    public KeyCode Left;
    public KeyCode Right;
    public float ForwardSpeed = 1;
    public float BackwardSpeed = 0.5f;
    public float StrafeSpeed = 0.75f;
    public float StrafeCutoff = 0.2f;
    public bool IsPlayer { get { return true; } }
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject MenuCanvas;

    private Rigidbody _rb;
    public Rigidbody Rb { get { return _rb; } }
    Unit _unit;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _unit = GetComponent<Unit>();
    }

    public void LockControls(bool locked)
    {
        _lockControls = locked;
        _rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        /*
        if (_lockControls || _unit.Stats.IsStunned) return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _unit.SwitchWeapon();
        }
        */
    }

    void DeconstructVelocityAndApplyToAnimation(Vector3 velocity)
    {
        float right = 0, fwd = 0, scale = 0;
        if (velocity != Vector3.zero)
        {
            scale = velocity.magnitude;
            float d = Vector3.Dot(transform.forward, velocity.normalized); // 1 = same dir, -1 = walking backwards
            float d1 = Vector3.Dot(transform.right, velocity.normalized); // 1 = walking right, -1 = walking left




            right = d1;
            fwd = d;


        }

        Animator.SetFloat("speed", 2+scale);
        Animator.SetFloat("forwards", fwd * Mathf.Clamp01(scale));
        Animator.SetFloat("right", right * Mathf.Clamp01(scale));
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DeconstructVelocityAndApplyToAnimation(_rb.velocity);
        if (_lockControls || _unit.Stats.IsStunned) return;
        Vector3 vDir = ViewingDirection();
        vDir = new Vector3(vDir.x, 0, vDir.z);


        _target = transform.position + vDir;
        vDir.Normalize();

        Debug.DrawRay(transform.position, vDir * 5, Color.blue);


        float speed;
        if (_rb.velocity == Vector3.zero)
        {
            speed = ForwardSpeed * _unit.Stats.CurrentMovementSpeed;
            transform.forward = transform.forward;
        }
        else
        {

            transform.forward = MakeControlsHardlyRetarded ? _rb.velocity : vDir;

            Debug.DrawRay(transform.position, _rb.velocity, Color.red);
            float d = Vector3.Dot(vDir, _rb.velocity.normalized);
            if (d < StrafeCutoff && d > -StrafeCutoff)
            {
                speed = StrafeSpeed * _unit.Stats.CurrentMovementSpeed;
            }
            else if (d < -StrafeCutoff)
            {
                speed = BackwardSpeed * _unit.Stats.CurrentMovementSpeed;
            }
            else
            {
                speed = ForwardSpeed * _unit.Stats.CurrentMovementSpeed;
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
        //anim.SetFloat("Forward", 0);
        if (v != Vector3.zero)
        {
            _rb.AddForce(v.normalized * speed, ForceMode.Acceleration);

            //anim.SetFloat("Forward", speed);

        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (MenuCanvas.activeSelf)
            {
                MenuCanvas.SetActive(false);
                GameOverScreen.SetActive(false);
                WinScreen.SetActive(false);
            }
            else
            {
                MenuCanvas.SetActive(true);
                GameOverScreen.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (MenuCanvas.activeSelf)
            {
                MenuCanvas.SetActive(false);
                GameOverScreen.SetActive(false);
                WinScreen.SetActive(false);
            }
            else
            {
                MenuCanvas.SetActive(true);
                WinScreen.SetActive(true);
            }
        }
    }


    Vector3 ViewingDirection()
    {
        if (MakeControlsHardlyRetarded)
        {
            return transform.forward;
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            Ray r = Camera.ScreenPointToRay(mousePos);
            RaycastHit info;
            if (Physics.Raycast(r, out info, 1000, 1 << 9))
            {
                return info.point - transform.position; //Position only because the camera is not a child object 
            }
            return -Vector3.one;
        }
    }

}
