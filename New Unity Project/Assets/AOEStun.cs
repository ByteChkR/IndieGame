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
    private List<AbstractEffect> _onHitEffects;
    [Tooltip("If the collider is a Sphere only the X axis will be used as a radius.")]
    public Vector3 HitboxSize;
    [SerializeField]
    private bool _inFrontOfPlayer = true;



    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);

        if (CollType == ColliderTypes.Box)
        {
            (Collider as BoxCollider).size = HitboxSize;
            if (_inFrontOfPlayer) (Collider as BoxCollider).center = Vector3.forward * HitboxSize.z / 2; //Move Hitbox right in front of caster
        }
        else if (CollType == ColliderTypes.Sphere)
        {
            (Collider as SphereCollider).radius = HitboxSize.x;
            if (_inFrontOfPlayer) (Collider as SphereCollider).center = Vector3.forward * HitboxSize.x / 2;
        }

        
        //if (Source.UnitAnimation[_animationName] != null)
        //{
        //    Source.UnitAnimation[_animationName].speed = _animationSpeed;
        //    Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        //}

        //Source.UnitAnimation.SetTrigger("attack");

    }
    float fml = 0;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        fml += Time.deltaTime;
        if (fml >= 1) Destroy(gameObject);
    }



    public override void OnDestroy()
    {

        base.OnDestroy();
    }




    public override void OnHit(Unit target)
    {
        Debug.Log(target.gameObject.name);
        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        base.OnHit(target);
    }


}
