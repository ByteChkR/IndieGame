using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Ability : MonoBehaviour
{

    protected Unit Source;
    [SerializeField]
    protected Collider _collider;
    protected bool Initialized = false;
    protected List<int> unitsHitSinceInit = new List<int>();
    [SerializeField]
    protected bool SelfStun = false;
    [SerializeField]
    protected bool CheckCollisionsEveryFrame = true;
    [SerializeField]
    protected float KnockBackPower = 0;
    [SerializeField]
    protected float KnockBackMaxRange = 10;
    [SerializeField]
    protected float KnockBackPowerOnMaxRange = 5;
    [SerializeField]
    protected bool InvertKnockback = false;
    [SerializeField]
    protected bool MoreOnMaxRange = false;

    public enum ColliderTypes { None, Box, Sphere }
    public ColliderTypes collType;
    // Use this for initialization
    void Start()
    {

    }

    public virtual void Initialize(int source, Vector3 target)
    {
        if (_collider == null)
        {
            _collider = GetComponent<Collider>();
        }
        if (_collider == null) collType = ColliderTypes.None;
        else if (_collider is SphereCollider) collType = ColliderTypes.Sphere;
        else if (_collider is BoxCollider) collType = ColliderTypes.Box;

        //Debug.Assert(_collider != null, "Ability has no Collider");

        Source = Unit.ActiveUnits[source];
        Initialized = true;
        Source.AddAnimationTriggerListener(CollisionCheck);

    }

    public virtual void OnHit(Unit target)
    {
        if (KnockBackPower != 0)
        {
            Vector3 d = (target.transform.position - Source.transform.position);
            d = InvertKnockback ? -d : d;
            float deltaPower = MoreOnMaxRange ? -(KnockBackPower - KnockBackPowerOnMaxRange) : KnockBackPower - KnockBackPowerOnMaxRange;
            float p = Mathf.Clamp01(d.magnitude / KnockBackMaxRange);
            target.controller.rb.AddForce(d.normalized * (KnockBackPowerOnMaxRange + p * deltaPower), ForceMode.VelocityChange);
        }
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (!Initialized) return;
        if (CheckCollisionsEveryFrame) CheckAndResolveCollisions(_collider);
    }

    public virtual void OnDestroy()
    {
        Source.RemoveAnimationTriggerListener(CollisionCheck);
    }

    void CollisionCheck(Unit.TriggerType ttype)
    {
        if (ttype != Unit.TriggerType.CollisionCheck) return;
        CheckAndResolveCollisions(_collider);
    }

    void CheckAndResolveCollisions(Collider coll)
    {
        List<int> newUnits = new List<int>();
        if (collType == ColliderTypes.None) return;
        switch (collType)
        {
            case ColliderTypes.Box:
                newUnits = CheckAndResolveCollisions(coll as BoxCollider);
                break;
            case ColliderTypes.Sphere:
                newUnits = CheckAndResolveCollisions(coll as SphereCollider);
                break;
            default:
                break;
        }
        unitsHitSinceInit.AddRange(newUnits);
    }

    List<int> CheckAndResolveCollisions(SphereCollider coll)
    {
        List<int> unitsHit = Physics.OverlapSphere(_collider.transform.position + coll.center, coll.radius, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();

        List<int> newUnits = unitsHit.Select(x => x).Where(x => !unitsHitSinceInit.Contains(x)).ToList();

        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        Debug.Log(newUnits.Count);
        return newUnits;
    }

    List<int> CheckAndResolveCollisions(BoxCollider coll)
    {
        List<int> unitsHit = Physics.OverlapBox(_collider.transform.position + coll.center, coll.size / 2, _collider.transform.rotation, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();

        List<int> newUnits = unitsHit.Select(x => x).Where(x => !unitsHitSinceInit.Contains(x)).ToList();

        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        Debug.Log(newUnits.Count);
        return newUnits;
    }
}
