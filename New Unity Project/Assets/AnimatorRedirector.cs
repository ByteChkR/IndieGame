using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRedirector : MonoBehaviour {

    Unit u;
	public void FireAnimationTrigger(int triggerType)
    {
        u.FireAnimationTrigger(triggerType);
    }
    private void Start()
    {
        u = GetComponentInParent<Unit>();
    }
}
