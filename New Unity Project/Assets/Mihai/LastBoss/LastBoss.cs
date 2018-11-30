using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour,IController{


    [Header("Parameters")]
    public float rotationInterpolationSpeed = 0.1f;
    public Transform looker;
    public GameObject InstanceiatorWithOffset;
    public enum LastBossStates { Attack1, Attack2, Attack3, Attack4}
    private LastBossStates _firstBossState = LastBossStates.Attack4;
    public Unit bossUnit;
    public Transform player;
    public float activationDistance;

    [Header("Positions")]
    public Transform fireRight;
    public Transform fireLeft;
    public Transform lowDir;
    public Transform leftDir;
    [Header("Prefabs")]
    public GameObject trioAttackPrefab;
    public GameObject normalAttack;
    public GameObject bounceAttack;

    private float _timeTillNextAttack = 2;
    private Rigidbody _rb;
    private bool isStarted = false;
    private bool finishedThirdAttack = false;
    private bool attack3tool = false; 

    public bool IsPlayer { get { return false; } }
    public Rigidbody Rb { get { return _rb; } }
    public void LockControls(bool locked)
    {

    }

    public Vector3 ViewingDirection(bool GetRelativeMousePos = false)
    {
        return transform.forward;
    }

    public Vector3 VTarget { get { return player == null ? Vector3.zero : player.position; } }

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        if (bossUnit == null)
        {
            bossUnit = GetComponent<Unit>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        HUDScript.instance.SetBoss(bossUnit);
        
    }
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            return;
        }

        if(isStarted == false)
        {
            if( Vector3.Distance(player.position,transform.position) < activationDistance)
            {
                isStarted = true;

            }
            return;
        }

        LookAfterPlayer();
        BossStateMachine();
	}


    private void LookAfterPlayer()
    {
        Vector3 sameheight = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        looker.LookAt(sameheight);
        transform.rotation = Quaternion.Lerp(transform.rotation, looker.rotation, rotationInterpolationSpeed);
    }

    private void BossStateMachine()
    {
        if (_timeTillNextAttack > 0)
        {
            _timeTillNextAttack -= Time.deltaTime;
        }
        else
        {
            switch (_firstBossState)
            {
                case LastBossStates.Attack1:
                    _timeTillNextAttack = 6;
                    InstanciateWithDelay(trioAttackPrefab, fireRight, 0.5f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireLeft, 0.8f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireRight, 1.4f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireLeft, 1.7f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireRight, 2.2f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireLeft, 2.5f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireRight, 3.0f, Vector3.zero);
                    InstanciateWithDelay(trioAttackPrefab, fireLeft, 3.3f, Vector3.zero);
                    _firstBossState = LastBossStates.Attack2;

                    break;
                case LastBossStates.Attack2:
                    _timeTillNextAttack = 3;
                    for (int i = 0; i < 18; i++)
                    {
                        if (i >= 8 && i <= 10)
                        {
                            InstanciateWithDelay(normalAttack, lowDir, i < 9 ? i * 0.15f + 0.5f : (18 - i) * 0.15f + 0.5f, new Vector3(-8 + i + 0.5f, 0, -10));
                        }
                        else
                        {
                            InstanciateWithDelay(normalAttack, lowDir, i < 10 ? i * 0.15f : (18 - i) * 0.15f, new Vector3(-8 + i + 0.5f, 0, -10));
                        }
                    }


                    _firstBossState = LastBossStates.Attack3;

                    break;
                case LastBossStates.Attack3:
                    Debug.Log("yes");
                        for (int j = 0; j < 5; j++)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (attack3tool == false)
                                {
                                    if (i == 5)
                                    {
                                       
                                        InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, -8 + 9 + 0.5f));
                                      
                                    }
                                    else
                                    {
                                        if (i % 2 == 1)
                                        {
                                            if (i == 1)
                                            {

                                                InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i : -8 + i + 4 + i - 6));
                                                InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i + 1 : -8 + i + 4 + i - 6 + 1));
                                            }
                                            else
                                            {
                                                 InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i * 2 : -8 + i + 4 + i - 6));
                                                  InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i * 2 + 1 : -8 + i + 4 + i - 6 + 1));
                                            }
                                        }
                                    }

                                }
                                else
                                {

                                    if (i % 2 == 0)
                                    {

                                        InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i * 2 : -8 + i + 4 + i - 6));
                                        InstanciateWithDelay(normalAttack, leftDir, j + 1, new Vector3(-10, 0, i < 5 ? -8 + i * 2 + 1 : -8 + i + 4 + i - 6 + 1));
                                        
                                    }

                                }


                            }
    
                                attack3tool = !attack3tool;

                        }


                        _firstBossState = LastBossStates.Attack4;
                        _timeTillNextAttack = 6;
                        
                    
                    break;
                case LastBossStates.Attack4:
                    _timeTillNextAttack = 10;

                    InstanciateWithDelay(bounceAttack, fireRight, 0.5f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireLeft, 0.8f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireRight, 1.4f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireLeft, 1.7f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireRight, 2.2f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireLeft, 2.5f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireRight, 3.0f, Vector3.zero);
                    InstanciateWithDelay(bounceAttack, fireLeft, 3.3f, Vector3.zero);

                    _firstBossState = LastBossStates.Attack1;
                    break;
            }

        }

    }

    public void  InstanciateWithDelay(GameObject pPrefab, Transform PTransform, float pTime, Vector3 pOptional)
    {
        GameObject iwto = Instantiate(InstanceiatorWithOffset,transform.position,transform.rotation);
        CreatorHelper creatorHelper = iwto.GetComponent<CreatorHelper>();
        creatorHelper.timeLeft = pTime;
        creatorHelper.prefab = pPrefab;
        creatorHelper.firePlaceTransform = PTransform;
        creatorHelper.positionOffset = pOptional;
        creatorHelper.SetReadyToStart();

       
    }

    public float GetDistance()
    {
        if(player == null)
        {
            Debug.Log("No player");
            return 0;
        }

        return Vector3.Distance(player.position, transform.position);
    }
}
