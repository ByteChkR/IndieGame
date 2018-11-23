using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Ability {

    public float Angle;
    public GameObject Projectile;
	// Use this for initialization
	void Start () {
		
	}

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        Shuriken s = Instantiate(Projectile, transform.position, transform.rotation).GetComponent<Shuriken>();
        s.Initialize(source, target, rot);
        Quaternion rt = transform.rotation;
        rot.eulerAngles = rot.eulerAngles + new Vector3(0, Angle,0);
        s = Instantiate(Projectile, transform.position, rot).GetComponent<Shuriken>();
        s.Initialize(source, target, rt);
        rot.eulerAngles = rot.eulerAngles - new Vector3(0, Angle*2, 0);
        s = Instantiate(Projectile, transform.position, rot).GetComponent<Shuriken>();
        s.Initialize(source, target, rt);
        Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }

    public override void Update()
    {
        base.Update();
    }


}
