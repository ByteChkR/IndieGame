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
    private float _comboGainPerHit = 1;


    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        Collider = Unit.ActiveUnits[source].GetActiveWeapon().Coll;
        base.Initialize(source, target, rot, isSpecial);
        

    }


    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (Source.GetAnimationState() != Unit.AnimationStates.ATTACK)
        {
            Destroy(gameObject);
        }
    }




    public override void OnDestroy()
    {
        base.OnDestroy();
    }


    public override void OnHit(Unit target)
    {
        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        base.OnHit(target);

        if (Source != null) Source.Stats.ApplyValue(Unit.StatType.COMBO, _comboGainPerHit, -1, false);
    }


}
