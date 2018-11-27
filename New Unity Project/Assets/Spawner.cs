using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Unit;
    bool spawning = false;
    public GameObject Weapon;
    Animation anim;
    public string AnimationName;
    public float DistanceAboveGround = 0.5f;
    public Renderer r;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartSpawn();
        if (spawning && !anim.isPlaying)
        {
            spawning = false;
            r.enabled = false;
        }
    }

    public void StartSpawn()
    {
        if (anim.isPlaying) return;
        spawning = true;
        r.enabled = true;
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
        Weapon w = Instantiate(Weapon, u.transform.position, Quaternion.identity, u.WeaponContainer.transform).GetComponent<Weapon>();
        u.PickupWeapon(w);
    }
}
