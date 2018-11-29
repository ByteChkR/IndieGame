using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : MonoBehaviour,IController {

    public float rotationInterpolationSpeed = 0.1f;
    public Transform looker;
    public Transform TrioPlace;

    public Animator _animator;
    public Animator Animator { get { return _animator; } }


    public GameObject TrioPrefab;

    private Quaternion _watchingDirection;
    private bool _canWatch = true;

    public Transform player;
    private Rigidbody _rb;
    public enum FirstBossStates { Dash, RangedAttack, Trio, Special}
    private FirstBossStates _firstBossState =FirstBossStates.Dash;
    public Unit bossUnit;

    public GameObject widePrefab;
    public GameObject specialPrefab;
    private float specialOffset= 15;
    private readonly float specialDegrees = 45;
    private bool _canBeStunned = false;
    private bool _canDealDashDamage = false;


    private Animator _anim;
    private int _animationResetTime = 0;
    private float _timeTillNextAttack = 1;

    public bool IsPlayer { get { return false; } }
    public Rigidbody Rb { get { return _rb; } }
    public void LockControls(bool locked)
    {
        
    }
    public Vector3 ViewingDirection(bool GetRelativeMousePos = false)
    {
        return transform.forward;
    }

    public Vector3 VTarget { get { return player ==null? Vector3.zero: player.position; } }


    void Start () {

        if(bossUnit == null)
        {
            bossUnit = GetComponent<Unit>();
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        HUDScript.instance.SetBoss(bossUnit);

	}
	
	// Update is called once per frame
	void Update () {

        WatchControl();
        AnimationControll();
        StunCheck();
        DashControl();
        // TestAnimation();
       
    }

    private void DashControl()
    {
        if(_rb.velocity.magnitude <2)
        {
            return;
        }

        Collider[] dashTest = Physics.OverlapSphere(transform.position, 0.5f);

        for (int i = 0; i < dashTest.Length; ++i)
        {
            if (dashTest[i].gameObject.tag == "Player")
            {

                if (_canDealDashDamage == true)
                {
                    _canDealDashDamage = false;
                    Unit playerUnit = dashTest[i].gameObject.GetComponent<Unit>();
                    if (playerUnit != null)
                    {
                        playerUnit.Stats.ApplyValue(Unit.StatType.HP, -20, 90,false);
                    }
                }

            }
        }

    }

    private void StunCheck()
    {
        if (_firstBossState == FirstBossStates.Dash)
        {
            
            if (_canBeStunned == true && bossUnit.Stats.IsStunned == true)
            {
                _canBeStunned = false;
                EndSpecial();

            }

        }
        else
        {

        }
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
            switch(_firstBossState)
            {
                case FirstBossStates.Dash:
                    _timeTillNextAttack = 6;
                    DoAnimation(1, 5);
                    _firstBossState = FirstBossStates.RangedAttack;
                    _canDealDashDamage = true;
                    _canBeStunned = false;
                    break;
                case FirstBossStates.RangedAttack:
                    _timeTillNextAttack = 3;
                    DoAnimation(2, 3);
                    _firstBossState = FirstBossStates.Trio;
                    _canDealDashDamage = false;
                    break;
                case FirstBossStates.Trio:
                    _firstBossState = FirstBossStates.Special;
                    _timeTillNextAttack = 6;
                    DoAnimation(4, 5);
                    break;
                case FirstBossStates.Special:
                    _timeTillNextAttack = 8;
                    _firstBossState = FirstBossStates.Dash;
                    _canBeStunned = true;
                    DoAnimation(3, 9999999);
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
            EndSpecial();
           /* _anim.SetInteger("frame", 1);
            _animationResetTime=5;*/
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
            if (player != null)
            {
                Vector3 samePosition = new Vector3(player.position.x, transform.position.y, player.position.z);
                looker.LookAt(samePosition);

                transform.rotation = Quaternion.Lerp(transform.rotation, looker.rotation, rotationInterpolationSpeed);
            }
        }
    }

    private void DashController()
    {

    }

    private void DashToPlayer()
    {
        _rb.velocity = transform.forward * 80;
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

    private void EndSpecial()
    {
        if (_firstBossState == FirstBossStates.Dash)
        {
          
            _animationResetTime = 2;
            _timeTillNextAttack = 3;
        }
    }

}
