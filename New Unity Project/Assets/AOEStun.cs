using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEStun : Ability
{
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

    bool started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target)
    {
        base.Initialize(source, target);
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
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (started && Source != null && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }



    public override void OnHit(Unit target)
    {
        target.stats.AddEffects(onHitEffects.ToArray());
        base.OnHit(target);
    }


}
