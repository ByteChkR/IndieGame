using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAttack : Ability
{
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private List<AbstractEffect> _onHitEffects;
    [SerializeField]
    private  float _comboGainPerHit = 100;


    private bool _started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        Collider = Unit.ActiveUnits[source].GetActiveWeapon().Coll;
        base.Initialize(source, target,rot);
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
        
        if (_started && Source != null && Source.Controller.Animator != null )
        {
            
            AnimatorStateInfo i = Source.Controller.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorTransitionInfo ti = Source.Controller.Animator.GetAnimatorTransitionInfo(0);
            if (!ti.IsUserName("2Attack") || !i.IsName("Attack")){
                _started = false;
                Destroy(gameObject);
            }
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
