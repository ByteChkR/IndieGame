using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAttack : AbstractAbilityInstance {
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;


    bool started = false;
    bool initialized = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void RegisterSource(int dummy)
    {
        base.RegisterSource(dummy);
        

        initialized = true;
        source.UnitAnimation[_animationName].speed = _animationSpeed;
        source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
    }

    // Update is called once per frame
    void Update () {
        if (!initialized)
        {
            return;
        }
        if(started && !source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
	}

    public override void OnHit(int id)
    {
        Unit.ActiveUnits[id].ApplyDamage(_damage);
        base.OnHit(id);
    }


}
