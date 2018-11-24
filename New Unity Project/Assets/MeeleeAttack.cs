using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAttack : Ability
{
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    [SerializeField]
    private float ComboGainPerHit = 100;


    bool started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        _collider = Unit.ActiveUnits[source].SelectedWeapon.coll;
        base.Initialize(source, target,rot);

        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (started && Source != null && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
    }


    public override void OnHit(Unit target)
    {
        if (Source != null) Source.stats.ApplyValue(Unit.StatType.COMBO, ComboGainPerHit);
        target.stats.AddEffects(onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        base.OnHit(target);
    }


}
