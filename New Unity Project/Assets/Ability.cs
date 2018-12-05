using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Ability : MonoBehaviour
{
    public float delayStart = 0;
    public bool DestroySourceOnEnd = false;
    public bool FixForPlayerModel = false;
    public static List<Ability> AliveAbilities = new List<Ability>();
    protected bool isSpecial = false;
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
    public bool UseMaxTime = false;
    public bool UseStateChange = true;
    public float MaxTimeAlive;
    public Unit.AnimationStates state = Unit.AnimationStates.ANY;
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

    public void SetAnimState(Unit.AnimationStates s)
    {
        state = s;
    }

    public void SetSpecialAttack(bool special)
    {
        isSpecial = special;
    }

    public virtual void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        
        SetSpecialAttack(this.isSpecial);
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
        

        if (SelfStun) Source.UnitController.LockControls(true);
        Initialized = true;
    }

    public virtual void OnHit(Unit target)
    {
        if (target.UnitController.IsPlayer)
        {
            Debug.Log("TEST");
            target.TriggerParticleEffect("hit");
        }
        if (KnockBackPower != 0)
        {
            Vector3 d = (target.transform.position - Source.transform.position);
            d = InvertKnockback ? -d : d;
            float deltaPower = MoreOnMaxRange ? -(KnockBackPower - KnockBackPowerOnMaxRange) : KnockBackPower - KnockBackPowerOnMaxRange;
            float p = Mathf.Clamp01(d.magnitude / KnockBackMaxRange);
            target.UnitController.Rb.AddForce(d.normalized * (KnockBackPowerOnMaxRange + p * deltaPower), ForceMode.VelocityChange);
        }

        target.Stats.ApplyValue(Unit.StatType.HP, -Damage, Source.gameObject.GetInstanceID(), isSpecial);
    }


    float t=0;
    // Update is called once per frame
    public virtual void Update()
    {
        
        if (!Initialized) return;
        if (Source != null && Collider != null && delayStart <=0) CheckAndResolveCollisions(Collider);


        Unit.AnimationStates s = Source.GetAnimationState();
        delayStart -= Time.deltaTime;
        t += Time.deltaTime;
        if ((UseMaxTime && t >= MaxTimeAlive))
        {
            Destroy(gameObject);

            
        }
        if((UseStateChange && state != Unit.AnimationStates.ANY && Source.GetAnimationState() != state))
        {
            
            Destroy(gameObject);
        }

    }

    public virtual void OnDestroy()
    {
        if (Source != null)
        {
            
            if (UnlockSelfStunOnDestroy) Source.UnitController.LockControls(false);
            if (DestroySourceOnEnd) Destroy(Source.gameObject);
            //Source.SetAnimationState(Unit.AnimationStates.IDLE);
        }
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
        Vector3 pos = FixForPlayerModel ? Collider.transform.position + coll.center : Collider.transform.position - coll.center;
        List<int> unitsHit = Physics.OverlapSphere(pos, coll.radius, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();

        List<int> newUnits = unitsHit.Select(x => x).
            Where(x => !UnitsHitSinceInit.Contains(x) && Unit.ActiveUnits[x].TeamID != Source.TeamID).
            ToList();

        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        //Debug.Log(unitsHit.Count);
        return newUnits;
    }

    List<int> CheckAndResolveCollisions(BoxCollider coll)
    {
        Vector3 pos = FixForPlayerModel ? Collider.transform.position + coll.center : Collider.transform.position - coll.center;
        List<int> unitsHit = Physics.OverlapBox(pos, coll.size / 2, Collider.transform.rotation, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();
        if (Source.gameObject.name == "Player" && unitsHit.Count > 0) Debug.Log(Unit.ActiveUnits[unitsHit[0]]);

        List<int> newUnits = unitsHit.Select(x => x).
            Where(x => !UnitsHitSinceInit.Contains(x) && Unit.ActiveUnits[x].TeamID != Source.TeamID).
            ToList();
        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));

        //Debug.Log(newUnits.Count);
        return newUnits;
    }
}
