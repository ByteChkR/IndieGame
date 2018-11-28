using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SejuR : Ability
{

    private bool _firstHit = true;
    public List<AbstractEffect> OnHitEffects;
    public float Speed = 1f;
    public AbstractEffect Stun;
    public float TravelDistance = 5f;
    public float MaxTime = 1f;
    private float _initTime;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);
        _initTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (UnitsHitSinceInit.Count > 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_firstHit)
        {
            transform.position = transform.position + transform.forward * Speed;

            TravelDistance -= Speed;
        }

        if (TravelDistance <= 0 || _initTime + MaxTime <= Time.realtimeSinceStartup) Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnHit(Unit target)
    {

        base.OnHit(target);
        if (_firstHit)
        {
            _firstHit = false;
            target.Stats.AddEffect(Stun, Source.gameObject.GetInstanceID());
        }
        target.Stats.AddEffects(OnHitEffects.ToArray(), Source.gameObject.GetInstanceID());
    }
}
