using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Ability : MonoBehaviour
{
    public static List<Ability> AliveAbilities = new List<Ability>();

    protected Unit Source;
    [SerializeField]
    protected Collider Collider;
    protected bool Initialized = false;
    protected List<int> UnitsHitSinceInit = new List<int>();
    [SerializeField]
    protected bool SelfStun = false;
    [SerializeField]
    protected bool UnlockSelfStunOnDestroy = true;
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
    private int _source;
    public enum ColliderTypes { None, Box, Sphere }
    public ColliderTypes CollType;
    protected Vector3 TargetPos;
    protected Quaternion TargetRot = Quaternion.identity;
    [SerializeField]
    protected float Damage;
    private void Awake()
    {
        AliveAbilities.Add(this);
    }
    // Use this for initialization
    void Start()
    {

    }

    public void RemoveAbility(int owner)
    {
        if (_source == owner)
        {
            AliveAbilities.Remove(this);
            Destroy(gameObject);
        }
    }

    public virtual void Initialize(int source, Vector3 target, Quaternion rot)
    {
        Debug.Log("Init Ability");
        this.TargetPos = target;
        this.TargetRot = rot;
        this._source = source;
        if (Collider == null)
        {
            Collider = GetComponent<Collider>();
        }
        if (Collider == null) CollType = ColliderTypes.None;
        else if (Collider is SphereCollider) CollType = ColliderTypes.Sphere;
        else if (Collider is BoxCollider) CollType = ColliderTypes.Box;

        //Debug.Assert(_collider != null, "Ability has no Collider");

        Source = Unit.ActiveUnits[source];
        Initialized = true;
        Source.AddAnimationTriggerListener(CollisionCheck);
        if (SelfStun) Source.FireAnimationTrigger(Unit.TriggerType.ControlLock);

    }

    public virtual void OnHit(Unit target)
    {
        if (KnockBackPower != 0)
        {
            Vector3 d = (target.transform.position - Source.transform.position);
            d = InvertKnockback ? -d : d;
            float deltaPower = MoreOnMaxRange ? -(KnockBackPower - KnockBackPowerOnMaxRange) : KnockBackPower - KnockBackPowerOnMaxRange;
            float p = Mathf.Clamp01(d.magnitude / KnockBackMaxRange);
            target.Controller.Rb.AddForce(d.normalized * (KnockBackPowerOnMaxRange + p * deltaPower), ForceMode.VelocityChange);
        }

        target.Stats.ApplyValue(Unit.StatType.HP, -Damage, Source.gameObject.GetInstanceID());
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (!Initialized) return;
        if (CheckCollisionsEveryFrame && Source != null && Collider != null) CheckAndResolveCollisions(Collider);
    }

    public virtual void OnDestroy()
    {
        if (Source != null)
        {
            Source.RemoveAnimationTriggerListener(CollisionCheck);
            if (UnlockSelfStunOnDestroy) Source.FireAnimationTrigger(Unit.TriggerType.ControlUnlock);
            
        }
        Debug.Log("Destroy Ability");
    }

    protected virtual void CollisionCheck(Unit.TriggerType ttype)
    {
        if (ttype != Unit.TriggerType.CollisionCheck) return;
        CheckAndResolveCollisions(Collider);
    }

    void CheckAndResolveCollisions(Collider coll)
    {
        List<int> newUnits = new List<int>();
        if (CollType == ColliderTypes.None) return;
        switch (CollType)
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
        UnitsHitSinceInit.AddRange(newUnits);
    }

    List<int> CheckAndResolveCollisions(SphereCollider coll)
    {
        List<int> unitsHit = Physics.OverlapSphere(Collider.transform.position + coll.center, coll.radius, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();

        List<int> newUnits = unitsHit.Select(x => x).Where(x => !UnitsHitSinceInit.Contains(x)).ToList();

        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        //Debug.Log(unitsHit.Count);
        return newUnits;
    }

    List<int> CheckAndResolveCollisions(BoxCollider coll)
    {
        List<int> unitsHit = Physics.OverlapBox(Collider.transform.position + coll.center, coll.size / 2, Collider.transform.rotation, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();

        List<int> newUnits = unitsHit.Select(x => x).Where(x => !UnitsHitSinceInit.Contains(x)).ToList();

        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        //Debug.Log(newUnits.Count);
        return newUnits;
    }
}
