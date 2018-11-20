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

    

    bool initialized = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void RegisterSource(int dummy)
    {
        base.RegisterSource(dummy);
        

        initialized = true;

    }

    // Update is called once per frame
    void Update () {
        if (!initialized)
        {
            return;
        }
        
	}

    public override void OnHit(int id)
    {
        Unit.ActiveUnits[id].ApplyDamage(_damage);
        base.OnHit(id);
    }


}
