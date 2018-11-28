using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRedirector : MonoBehaviour {

    Unit u;
	public void FireAnimationTrigger(int triggerType)
    {
        u.FireAnimationTrigger(triggerType);
    }

    public void EndAttackAnimation()
    {
        u.Controller.LockControls(false);
        Debug.Log("Unlocked");
        u.FireAnimationTrigger(Unit.TriggerType.EndAnimation);
        u.SetAnimationState(Unit.AnimationStates.IDLE);
    }


    private void Start()
    {
        u = GetComponentInParent<Unit>();
    }
}
