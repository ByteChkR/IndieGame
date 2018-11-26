using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryDrop : Ability
{

    public float DistanceFromCaster = 8;
    public Vector3 StartHeight;
    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);

        transform.position = target + StartHeight;
    }

    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    RaycastHit info;
    bool hit;
    public override void Update()
    {
        base.Update();
        hit = Physics.Raycast(new Ray(transform.position, Vector3.down), out info, 1000, 1 << 10);
        if ((hit && info.distance < 0.5f) || !hit) Destroy(gameObject);
    }
}

