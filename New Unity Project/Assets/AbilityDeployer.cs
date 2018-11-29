using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDeployer : Ability
{

    [Tooltip("If the collider is a Sphere only the X axis will be used as a radius.")]
    public Vector3 HitboxSize;
    [SerializeField]
    private bool _inFrontOfPlayer = true;
    [SerializeField]
    private GameObject ability;
    [SerializeField]
    private GameObject ownAbility;

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
            if (_inFrontOfPlayer) (Collider as BoxCollider).center = transform.forward * HitboxSize.z / 2; //Move Hitbox right in front of caster
        }
        else if (CollType == ColliderTypes.Sphere)
        {
            (Collider as SphereCollider).radius = HitboxSize.x;
            if (_inFrontOfPlayer) (Collider as SphereCollider).center = transform.forward * HitboxSize.x / 2;
        }
      
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        else if (Source == null) Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        if (Source != null && ownAbility != null)
        {
            Ability a = Instantiate(ownAbility, Source.transform.position, Source.transform.rotation).GetComponent<Ability>();
            a.Initialize(Source.gameObject.GetInstanceID(), Source.transform.position, Source.transform.rotation, isSpecial);

        }

        base.OnDestroy();
    }



    public override void OnHit(Unit target)
    {
        base.OnHit(target);
        Vector3 dir = target.transform.position - Source.transform.position;
        Ability a = Instantiate(ability, target.transform.position, target.transform.rotation).GetComponent<Ability>();
        a.Initialize(target.gameObject.GetInstanceID(), Source.transform.position + dir.normalized * 3, target.transform.rotation, isSpecial);

    }


}
