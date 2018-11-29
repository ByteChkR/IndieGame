using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> _onHitEffects;
    [SerializeField, Tooltip("The delay after the start of the animation")]
    private float TeleportTime = 0.3f;


    [SerializeField]
    private GameObject _nextAbility;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);

        Source.UnitAnimation.speed = _animationSpeed;
        
    }

    public void TriggerTeleport()
    {

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
        
    }

    float aa = 0;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        aa += Time.deltaTime;
        if(aa >= TeleportTime)
        {
            TriggerTeleport();
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
            a.SetAnimState(state);
            a.Initialize(Source.gameObject.GetInstanceID(), TargetPos, TargetRot, isSpecial);
        }
        base.OnDestroy();
    }


}
