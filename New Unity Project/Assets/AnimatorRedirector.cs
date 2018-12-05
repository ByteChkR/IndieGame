using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRedirector : MonoBehaviour {

    Unit u;
	public void FireAnimationTrigger(int triggerType)
    {
        //Debug.LogWarning("ANIMATION FIRED LEGACY ANIMATION TRIGGER");
        //u.FireAnimationTrigger(triggerType);
    }

    public void EndAttackAnimation()
    {
        
        return;

    }


    private void Start()
    {
        u = GetComponentInParent<Unit>();
    }
}
