using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAttack : Ability {
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    [SerializeField]
    private float ComboGainPerHit = 100;


    bool started = false;
    bool initialized = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void Initialize(int source, Vector3 target)
    {
        
        base.Initialize(source, target);
        
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
    }

    // Update is called once per frame
    void Update () {
        if (!initialized)
        {
            return;
        }
        if(started && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
	}

    

    public override void OnHit(Unit target)
    {
        target.stats.ApplyValue(Unit.StatType.HP, -_damage);
        target.stats.ApplyValue(Unit.StatType.COMBO, ComboGainPerHit);
        base.OnHit(target);
    }


}
