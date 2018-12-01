using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Ability
{

    public float StartSpeed;
    private float _spe;
    public float SpeedStepPerFrame;
    public float MinDistance;
    public Unit Target;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(12, 11);

        Physics.IgnoreLayerCollision(12, 12);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Target == null) return;
        float distance;

        Vector3 vdir = (Target.transform.position - transform.position);
        distance = vdir.magnitude;
        if(distance <= MinDistance)
        {
            if(Target.gameObject.tag == "Player")
            {
                AchievementSystem.instance.PickUpCoin();
            }
            Target.Stats.ApplyValue(Unit.StatType.GOLD, 1,-1,false);
            Destroy(gameObject);
        }

        vdir.Normalize();
        transform.position += vdir * StartSpeed * distance;
        StartSpeed += SpeedStepPerFrame;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);
        this.Target = Source;
    }

    public override void OnHit(Unit target)
    {
        
        base.OnHit(target);
        
    }
}
