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
    private List<AbstractEffect> _onHitEffects;
    [SerializeField]
    private readonly float _comboGainPerHit = 100;


    private bool _started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        Collider = Unit.ActiveUnits[source].SelectedWeapon.Coll;
        base.Initialize(source, target,rot);
        if (Source.UnitAnimation[_animationName] != null)
        {
            Source.UnitAnimation[_animationName].speed = _animationSpeed;
            Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        }
        _started = true;
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (_started && Source != null && !Source.UnitAnimation.isPlaying)
        {
            _started = false;
            Destroy(this.gameObject);
        }
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
    }


    public override void OnHit(Unit target)
    {
        if (Source != null) Source.Stats.ApplyValue(Unit.StatType.COMBO, _comboGainPerHit);
        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        base.OnHit(target);
    }


}
