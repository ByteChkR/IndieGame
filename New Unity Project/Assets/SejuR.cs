using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SejuR : Ability
{

    bool firstHit = true;
    public List<AbstractEffect> onHitEffects;
    public float speed = 1f;
    public AbstractEffect stun;
    public float TravelDistance = 5f;
    public float maxTime = 1f;
    float initTime;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target)
    {
        base.Initialize(source, target);
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
        if (firstHit)
        {
            transform.position = transform.position + transform.forward * speed;

            TravelDistance -= speed;
        }

        if (TravelDistance <= 0 || initTime + maxTime <= Time.realtimeSinceStartup) Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnHit(Unit target)
    {

        base.OnHit(target);
        if (firstHit)
        {
            firstHit = false;
            target.stats.AddEffect(stun);
        }
        target.stats.AddEffects(onHitEffects.ToArray());
    }
}
