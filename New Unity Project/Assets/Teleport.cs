using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability {
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    bool blinked = false;

    bool started = false;
    Vector3 target;
	// Use this for initialization
	void Start () {
		
	}

    public override void Initialize(int source, Vector3 target)
    {
        base.Initialize(source, target);
        this.target = target;
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        
    }

    public void TriggerTeleport()
    {
        Source.eventListener -= TriggerTeleport;
        RaycastHit info;
        Vector3 pos;
        if(Physics.Raycast(target, -transform.forward, out info, 1.5f))
        {
            pos = target + transform.forward * 1.5f;
        }
        else
        {
            pos = target - transform.forward * 1.5f;
        }
        Source.transform.position = pos;
        Source.agent.isStopped = true;
        blinked = true;
    }

    // Update is called once per frame
    public override void Update () {
        if (!Initialized)
        {
            return;
        }
        if(blinked && started && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
	}


}
