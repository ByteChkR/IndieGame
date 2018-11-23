using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{
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

    [SerializeField]
    private GameObject _nextAbility;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        Source.AddAnimationTriggerListener(TriggerTeleport);
        started = true;
    }

    public void TriggerTeleport(Unit.TriggerType triggerType)
    {
        if (triggerType != Unit.TriggerType.Teleport) return;
        Source.RemoveAnimationTriggerListener(TriggerTeleport);
        RaycastHit info;
        Vector3 pos;
        Vector3 fwd = targetRot * Vector3.forward;
        if (Physics.Raycast(targetPos, -fwd, out info, 1.5f))
        {
            pos = targetPos + fwd * 1.5f;
        }
        else
        {
            pos = targetPos - fwd * 1.5f;
        }
        Source.transform.position = pos;
        if (Source.agent != null) Source.agent.isStopped = true;
        blinked = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (blinked && started && Source != null && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            if (_nextAbility != null)
            {
                Source.UnitAnimation.Stop();
                Ability a = Instantiate(_nextAbility, Source.transform.position, Source.transform.rotation).GetComponent<Ability>();
                a.Initialize(Source.gameObject.GetInstanceID(), targetPos, targetRot);
            }
            Destroy(this.gameObject);

        }
    }
    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }


}
