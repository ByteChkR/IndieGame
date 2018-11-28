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
    private List<AbstractEffect> _onHitEffects;
    private bool _blinked = false;

    private bool _started = false;

    [SerializeField]
    private GameObject _nextAbility;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);

        Source.UnitAnimation.speed = _animationSpeed;
        Source.AddAnimationTriggerListener(TriggerTeleport);
        _started = true;
    }

    public void TriggerTeleport(Unit.TriggerType triggerType)
    {
        if (triggerType != Unit.TriggerType.Teleport) return;
        Source.RemoveAnimationTriggerListener(TriggerTeleport);
        RaycastHit info;
        Vector3 pos;
        Vector3 fwd = TargetRot * Vector3.forward;
        if (Physics.Raycast(TargetPos, -fwd, out info, 1.5f))
        {
            pos = TargetPos + fwd * 1.5f;
        }
        else
        {
            pos = TargetPos - fwd * 1.5f;
        }
        Source.transform.position = pos;
        if (Source.Agent != null) Source.Agent.isStopped = true;
        _blinked = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }

    }
    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }

    public override void OnDestroy()
    {
        if (_nextAbility != null)
        {

            Ability a = Instantiate(_nextAbility, Source.transform.position, Source.transform.rotation).GetComponent<Ability>();
            a.Initialize(Source.gameObject.GetInstanceID(), TargetPos, TargetRot);
        }
        base.OnDestroy();
    }


}
