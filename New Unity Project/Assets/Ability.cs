using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Ability : MonoBehaviour
{

    protected Unit Source;
    [SerializeField]
    protected BoxCollider _collider;
    protected bool Initialized = false;
    private List<int> unitsHitSinceInit = new List<int>();
    // Use this for initialization
    void Start()
    {

    }

    public virtual void Initialize(int source, Vector3 target)
    {
        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider>();
        }

        //Debug.Assert(_collider != null, "Ability has no Collider");

        Source = Unit.ActiveUnits[source];
        Initialized = true;
        Source.AddAnimationTriggerListener(CollisionCheck);
    }

    public virtual void OnHit(Unit target)
    {

    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (!Initialized) return;
        CheckCollisions();
    }

    void CollisionCheck(Unit.TriggerType ttype)
    {
        if (ttype != Unit.TriggerType.CollisionCheck) return;
        CheckCollisions();
    }

    void CheckCollisions()
    {
        if (_collider == null) return;
        List<int> unitsHit = Physics.OverlapBox(_collider.transform.position + _collider.center, _collider.size / 2, _collider.transform.rotation, 1 << 11)
        .Select(x => x.gameObject.GetInstanceID())
        .Where(x => x != Source.gameObject.GetInstanceID()).ToList();
        List<int> newUnits = unitsHit.Select(x => x).Where(x => !unitsHitSinceInit.Contains(x)).ToList();
        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));
        unitsHitSinceInit.AddRange(newUnits);
    }
}
