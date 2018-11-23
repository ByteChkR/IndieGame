using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogMawR : Ability
{

    public AbstractEffect[] effects;
    public Vector3 Offset;
    // Use this for initialization
    void Start()
    {

    }

    RaycastHit info;
    bool hit;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        hit = Physics.Raycast(new Ray(transform.position, Vector3.down), out info, 1000, 1 << 10);
        if ((hit && info.distance < 0.5f) || !hit) Destroy(gameObject);
    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        transform.position = target + Offset;
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void OnHit(Unit target)
    {
        base.OnHit(target);
        target.stats.AddEffects(effects);
    }
}
