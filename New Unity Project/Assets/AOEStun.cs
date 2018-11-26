using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEStun : Ability
{
    [SerializeField]
    private readonly string _animationName;
    [SerializeField]
    private readonly float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> _onHitEffects;
    [Tooltip("If the collider is a Sphere only the X axis will be used as a radius.")]
    public Vector3 HitboxSize;
    [SerializeField]
    private readonly bool _inFrontOfPlayer = true;

    bool _started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        base.Initialize(source, target, rot);
        if (CollType == ColliderTypes.Box)
        {
            (Collider as BoxCollider).size = HitboxSize;
            if (_inFrontOfPlayer) (Collider as BoxCollider).center = transform.forward * HitboxSize.z / 2; //Move Hitbox right in front of caster
        }
        else if (CollType == ColliderTypes.Sphere)
        {
            (Collider as SphereCollider).radius = HitboxSize.x;
            if (_inFrontOfPlayer) (Collider as SphereCollider).center = transform.forward * HitboxSize.x / 2;
        }
        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        _started = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (_started && Source != null && !Source.UnitAnimation.isPlaying)
        {
            _started = false;
            Destroy(this.gameObject);
        }
        else if (Source == null) Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }



    public override void OnHit(Unit target)
    {
        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        base.OnHit(target);
    }


}
