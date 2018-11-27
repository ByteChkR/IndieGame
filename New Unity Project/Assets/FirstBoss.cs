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
    public FirstBossStates firstBossState =FirstBossStates.Dash;

    public GameObject widePrefab;
    public GameObject specialPrefab;
    private float specialOffset= 15;
    private readonly float specialDegrees = 45;

    private Animator _anim;
    private int _animationResetTime = 0;
    private float _timeTillNextAttack = 3;


	void Start () {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        WatchControl();
        AnimationControll();
     // TestAnimation();
           
	}

    private void AnimationControll()
    {
        
        if (_animationResetTime > 0)
        {
            if (_animationResetTime == 1)
            {
                _anim.SetInteger("frame", 0);
            }
            _animationResetTime--;
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("FBIdle"))
        {
            _canWatch = true;
        }
        else
        {
            _canWatch = false;
        }
        
        if (_timeTillNextAttack>0)
        {
            _timeTillNextAttack-= Time.deltaTime;
        }
        else
        {
            switch(firstBossState)
            {
                case FirstBossStates.Dash:
                    _timeTillNextAttack = 6;
                    DoAnimation(1, 5);
                    firstBossState = FirstBossStates.RangedAttack;
                    break;
                case FirstBossStates.RangedAttack:
                    _timeTillNextAttack = 3;
                    DoAnimation(2, 3);
                    firstBossState = FirstBossStates.Trio;
                    break;
                case FirstBossStates.Trio:
                    firstBossState = FirstBossStates.Special;
                    _timeTillNextAttack = 6;
                    DoAnimation(4, 5);
                    break;
                case FirstBossStates.Special:
                    _timeTillNextAttack = 8;
                    firstBossState = FirstBossStates.Dash;
                    DoAnimation(3, 5);
                    break;
            }

        }

    }

    private void DoAnimation(int pIndex,int pResetFrames)
    {
        _anim.SetInteger("frame", pIndex);
        _animationResetTime = pResetFrames;
    }

    private void TestAnimation()
    {
       
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _anim.SetInteger("frame", 1);
            _animationResetTime=5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _anim.SetInteger("frame", 2);
            _animationResetTime = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _anim.SetInteger("frame", 3);
            _animationResetTime = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _anim.SetInteger("frame", 4);
            _animationResetTime = 5;
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
        trio.GetComponent<FirstBossTrioBullet>().player = player;

        GameObject trio2 = Instantiate(TrioPrefab, TrioPlace.position, TrioPlace.rotation);
        trio2.transform.Rotate(0, 30, 0);
        trio2.GetComponent<FirstBossTrioBullet>().player = player;

        GameObject trio3 = Instantiate(TrioPrefab, TrioPlace.position, TrioPlace.rotation);
        trio3.transform.Rotate(0, -30, 0);
        trio3.GetComponent<FirstBossTrioBullet>().player = player;
    }

    private void WideAttack()
    {
        Instantiate(widePrefab, transform.position, transform.rotation);
    }

    private void SpecialAttack()
    {
        for(int i = 1; i <7; i++)
        {
            GameObject specialAttack = Instantiate(specialPrefab, transform.position, transform.rotation);
            specialAttack.transform.Rotate(0, specialDegrees * i + specialOffset, 0);
            specialOffset += 15;
        }


    }
}
