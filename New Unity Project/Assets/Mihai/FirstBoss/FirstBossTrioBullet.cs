using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossTrioBullet : MonoBehaviour {

    public float speed = 5;
    private Rigidbody _rb;
    public float timeSpentOnWall = 2;
    private bool _hitWall = false;
    private bool _setDirection = false;
    public Transform player;

	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(player==null)
        {
            return;
        }

        if(_hitWall == true)
        {
            if(timeSpentOnWall>0)
            {
                timeSpentOnWall -= Time.deltaTime;
                _rb.velocity =Vector3.zero;
            }
            else
            {
                if(_setDirection == false)
                {
                    Vector3 playerRelativePosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                    transform.LookAt(playerRelativePosition);
                    _setDirection = true;
                }
                else
                {
                    _rb.velocity = transform.forward * speed;
                }
            }
            return;
        }

        _rb.velocity = transform.forward * speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FirstBossWall")
        {
            _hitWall = true;
        }
    }
}
