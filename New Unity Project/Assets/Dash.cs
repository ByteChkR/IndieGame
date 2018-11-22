using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    [Tooltip("If the collider is a Sphere only the X axis will be used as a radius.")]
    public Vector3 hitboxSize;
    [SerializeField]
    private bool InFrontOfPlayer = true;
    [SerializeField]
    private float DashDistance = 10;
    [SerializeField]
    private float DashTime = 1;
    [SerializeField]
    private AnimationCurve animationCurve;
    float timeInitialized;
    Vector3 pos;
    Vector3 endPos;
    bool started = false;
    float ddistance;
    public GameObject afterDashAbility;
    Vector3 target;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target)
    {
        this.target = target;
        base.Initialize(source, target);
        transform.parent = Source.transform;
        timeInitialized = Time.realtimeSinceStartup;
        if (collType == ColliderTypes.Box)
        {
            (_collider as BoxCollider).size = hitboxSize;
            if (InFrontOfPlayer) (_collider as BoxCollider).center = transform.forward * hitboxSize.z / 2; //Move Hitbox right in front of caster
        }
        else if (collType == ColliderTypes.Sphere)
        {
            (_collider as SphereCollider).radius = hitboxSize.x;
            if (InFrontOfPlayer) (_collider as SphereCollider).center = transform.forward * hitboxSize.x / 2;
        }
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
        pos = Source.transform.position;
        ddistance = (target - Source.transform.position).magnitude;
        Vector3 fwd = Source.transform.forward;
        fwd.y = 0;
        fwd.Normalize();
        endPos = ddistance > DashDistance ? Source.transform.position + fwd * DashDistance : Source.transform.position + fwd * ddistance;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float t = (Time.realtimeSinceStartup - timeInitialized) / DashTime;
        if (!Initialized)
        {
            return;
        }
        if (t >= 1)
        {
            AOEStun s = Instantiate(afterDashAbility, Source.transform.position, Source.transform.rotation).GetComponent<AOEStun>();
            s.Initialize(Source.gameObject.GetInstanceID(), target);
            started = false;
            Destroy(this.gameObject);
        }
        else
        {
            Source.transform.position = Vector3.Lerp(pos, endPos, animationCurve.Evaluate(t));
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }



    public override void OnHit(Unit target)
    {
        target.stats.ApplyValue(Unit.StatType.HP, -_damage);
        target.stats.AddEffects(onHitEffects.ToArray());
        base.OnHit(target);
    }


}
