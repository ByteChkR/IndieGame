using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : Ability {

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);
        SetAnimState(Unit.AnimationStates.DEATH);
        Source.SetAnimationState(Unit.AnimationStates.DEATH);
    }

    public override void OnDestroy()
    {

        base.OnDestroy();
        
    }

    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }

    public override void Update()
    {
        base.Update();
    }
}
