using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbstractAbilityInstance {
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
    bool initialized = false;
	// Use this for initialization
	void Start () {
		
	}

    public override void RegisterSource(int dummy)
    {
        base.RegisterSource(dummy);
        

        initialized = true;
        source.eventListener += TriggerTeleport;
        source.UnitAnimation[_animationName].speed = _animationSpeed;
        source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
    }

    public void TriggerTeleport()
    {
        source.eventListener -= TriggerTeleport;
        RaycastHit info;
        Vector3 pos;
        if(Physics.Raycast(target.position, -target.forward, out info, 1.5f))
        {
            pos = target.position + target.forward * 1.5f;
        }
        else
        {
            pos = target.position - target.forward * 1.5f;
        }
        source.transform.position = pos;
        source.agent.isStopped = true;
        blinked = true;
    }

    // Update is called once per frame
    void Update () {
        if (!initialized)
        {
            return;
        }
        if(blinked && started && !source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
	}

    public override void OnHit(int id)
    {
    }


}
