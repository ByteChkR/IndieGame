using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    [SerializeField]
    private  string _animationName;
    [SerializeField]
    private  float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> _onHitEffects;
    [Tooltip("If the collider is a Sphere only the X axis will be used as a radius.")]
    public Vector3 hitboxSize;
    [SerializeField]
    private  bool _inFrontOfPlayer = true;
    [SerializeField]
    private  float _dashDistance = 10000;
    [SerializeField]
    private  float _dashTime = 1;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private float _timeInitialized;
    private Vector3 _pos;
    private Vector3 _endPos;
    float _ddistance;
    public GameObject AfterDashAbility;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        transform.parent = Source.transform;
        _timeInitialized = Time.realtimeSinceStartup;
        if (CollType == ColliderTypes.Box)
        {
            (Collider as BoxCollider).size = hitboxSize;
            if (_inFrontOfPlayer) (Collider as BoxCollider).center = transform.forward * hitboxSize.z / 2; //Move Hitbox right in front of caster
        }
        else if (CollType == ColliderTypes.Sphere)
        {
            (Collider as SphereCollider).radius = hitboxSize.x;
            if (_inFrontOfPlayer) (Collider as SphereCollider).center = transform.forward * hitboxSize.x / 2;
        }
        Source.UnitAnimation.SetTrigger(_animationName);
        Vector3 fwd = (target - Source.transform.position);
        _pos = Source.transform.position;
        _ddistance = (fwd).magnitude;
        fwd.y = 0;
        fwd.Normalize();
        _endPos = _ddistance > _dashDistance ? Source.transform.position + fwd * _dashDistance : Source.transform.position + fwd * _ddistance;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float t = (Time.realtimeSinceStartup - _timeInitialized) / _dashTime;
        if (!Initialized)
        {
            return;
        }
        Debug.DrawLine(_pos, _endPos);
        if (t >= 1)
        {
            if (Source != null && AfterDashAbility != null)
            {
                AOEStun s = Instantiate(AfterDashAbility, Source.transform.position, Source.transform.rotation).GetComponent<AOEStun>();
                s.Initialize(Source.gameObject.GetInstanceID(), TargetPos, TargetRot);
            }

            Destroy(this.gameObject);
        }
        else
        {
            if (Source != null) Source.transform.position = Vector3.Lerp(_pos, _endPos, _animationCurve.Evaluate(t));
        }
    }

    public override void OnDestroy()
    {
        Source.rb.velocity = Vector3.zero;
        base.OnDestroy();
    }



    public override void OnHit(Unit target)
    {
        base.OnHit(target);

        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
    }


}
