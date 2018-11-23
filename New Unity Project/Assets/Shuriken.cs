using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Ability
{

    public List<AbstractEffect> onHitEffects;
    public float speed = 1f;
    public float TravelDistance = 5f;
    public float maxTime = 1f;
    float initTime;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        initTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (unitsHitSinceInit.Count > 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (unitsHitSinceInit.Count == 0)
        {
            transform.position = transform.position + transform.forward * speed;

            TravelDistance -= speed;
        }

        if (TravelDistance <= 0 || initTime + maxTime <= Time.realtimeSinceStartup) Destroy(gameObject);
    }

    public override void OnHit(Unit target)
    {
        target.stats.AddEffects(onHitEffects.ToArray());

        base.OnHit(target);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
