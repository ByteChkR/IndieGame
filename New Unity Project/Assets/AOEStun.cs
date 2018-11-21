using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEStun : Ability {
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    public Vector3 hitboxSize;

    bool started = false;
	// Use this for initialization
	void Start () {
		
	}
    
    public override void Initialize(int source, Vector3 target)
    {
        base.Initialize(source, target);
        
        _collider.size = hitboxSize;
        _collider.center = transform.forward*hitboxSize.z/2; //Move Hitbox right in front of caster
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if(started && !Source.UnitAnimation.isPlaying)
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
        target.stats.ApplyValue(Unit.StatType.HP, -_damage);
        target.stats.AddEffects(onHitEffects.ToArray());
        base.OnHit(target);
    }


}
