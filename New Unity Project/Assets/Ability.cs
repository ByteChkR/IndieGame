using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Ability : MonoBehaviour {

    protected Unit Source;
    [SerializeField]
    private BoxCollider _collider;
    private bool Initialized = false;
    private List<int> unitsHitSinceInit = new List<int>();
	// Use this for initialization
	void Start ()
    {
        if(_collider == null)
        {
            _collider = GetComponent<BoxCollider>();
        }

        Debug.Assert(_collider != null, "Ability has no Collider");

    }
	
    public virtual void Initialize(int source, Vector3 target)
    {
        Source = Unit.ActiveUnits[source];
        Initialized = true;
    }

    public virtual void OnHit(Unit target)
    {

    }


	// Update is called once per frame
	void Update ()
    {
        if (!Initialized) return;
        CheckCollisions();
	}

    void CheckCollisions()
    {
        List<int> unitsHit = Physics.OverlapBox(_collider.center, _collider.size / 2, _collider.transform.rotation).Select(x => x.gameObject.GetInstanceID()).ToList();
        List<int> newUnits = unitsHit.Select(x => x).Where(x => !unitsHitSinceInit.Contains(x)).ToList();
        newUnits.ForEach(x => OnHit(Unit.ActiveUnits[x]));
        unitsHitSinceInit.AddRange(newUnits);
    }
}
