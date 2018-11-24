using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour {

    public float rotationInterpolationSpeed = 0.1f;
    public Transform looker;
    public Transform TrioPlace;


    public GameObject TrioPrefab;

    private Quaternion _watchingDirection;
    private bool _canWatch = true;

    public Transform player;
    private Rigidbody _rb;
    public enum FirstBossStates { Dash, RangedAttack, Trio, Special}

    private Animator _anim;
    private int _maxNumberOfDashes = 3;
    private int _dashesLeft = 0;
    private float _dashMaxCooldown;
    private float _dashCurrentCooldwon;
    private int test = 0;
    private float _timeTillNextAttack = 3;


	void Start () {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        WatchControl();
        if(test > 0)
        {
            if(test == 1)
            {
                _anim.SetInteger("frame", 0);
            }
            test--;
        }
            TestAnimation();

	}
    private void TestAnimation()
    {
       
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _anim.SetInteger("frame", 1);
            test=5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _anim.SetInteger("frame", 2);
            test = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _anim.SetInteger("frame", 3);
            test = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _anim.SetInteger("frame", 4);
            test = 5;
        }
    }
    

    private void WatchControl()
    {
        if(_canWatch == true)
        {
            Vector3 samePosition = new Vector3(player.position.x, transform.position.y, player.position.z);
           looker.LookAt(samePosition);
           transform.rotation = Quaternion.Lerp(transform.rotation, looker.rotation, rotationInterpolationSpeed);
        }
    }

    private void DashController()
    {

    }

    private void DashToPlayer()
    {
        _rb.velocity = transform.forward * 30;
    }

    private void TrioAttack()
    {
        GameObject trio = Instantiate(TrioPrefab, TrioPlace.position, TrioPlace.rotation);
        GameObject trio2 = Instantiate(TrioPrefab, TrioPlace.position, TrioPlace.rotation);
        trio2.transform.Rotate(0, 20, 0);
        GameObject trio3 = Instantiate(TrioPrefab, TrioPlace.position, TrioPlace.rotation);
        trio3.transform.Rotate(0, -20, 0);
    }
}
