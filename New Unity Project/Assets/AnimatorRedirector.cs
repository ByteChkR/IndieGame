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
        u.SetAnimationState(Unit.AnimationStates.IDLE);
        u.Controller.LockControls(false);
        Debug.Log("Unlocked");
    }


    private void Start()
    {
        u = GetComponentInParent<Unit>();
    }
}
