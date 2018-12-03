using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner:MonoBehaviour
{


    public GameObject Unit;
    bool spawning = false;
    Animation anim;
    public GameObject[] Weapons;
    public static PlayerSpawner instance;
    public int WeaponID;
    public bool Spawned = false;
    public GameObject pipe;
    public string AnimationName;
    public float DistanceAboveGround = 0.5f;
    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) StartSpawn();
        if (spawning && !anim.isPlaying)
        {
            spawning = false;
            pipe.SetActive(false);
        }
    }

    public void StartSpawn(int WeaponID)
    {
        this.WeaponID = WeaponID;
        if (anim.isPlaying) return;
        spawning = true;
        pipe.SetActive(true);
        anim.Play(AnimationName, PlayMode.StopSameLayer);
        RaycastHit info;
        if (Physics.Raycast(transform.position, Vector3.down, out info, float.MaxValue, 1 << 10))
        {
            groundOffset = Vector3.down * (info.distance - DistanceAboveGround);
        }
        else groundOffset = Vector3.zero;
    }
    Vector3 groundOffset;
    void TriggerSpawn()
    {
        Unit u = Instantiate(Unit, transform.position+groundOffset, transform.rotation).GetComponent<Unit>();
        Weapon w = Instantiate(Weapons[WeaponID], u.transform.position, Quaternion.identity, u.WeaponContainer.transform).GetComponent<Weapon>();
        u.PickupWeapon(w);
        Spawned = true;
    }
}
