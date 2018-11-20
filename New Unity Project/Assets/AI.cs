using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    public float AttackRange;
    public float ActivationRange;
    float distance2Target;
    Unit u;
    bool sawTarget = false;
    bool CanSeeTarget
    {
        get
        {
            RaycastHit info;
            bool hit = Physics.Raycast(transform.position+(target.position - transform.position).normalized, target.position - transform.position, out info, ActivationRange);
            Debug.DrawRay(transform.position + (target.position - transform.position).normalized, target.position - transform.position);
            distance2Target = hit ? info.distance : float.MaxValue;
            return hit && info.collider.gameObject.GetInstanceID() == target.gameObject.GetInstanceID();
        }
    }
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        u = Unit.ActiveUnits[gameObject.GetInstanceID()];
        
	}

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (!CanSeeTarget)
        {
            return;
        }

            agent.SetDestination(target.position);
            agent.isStopped = false;
        if(distance2Target <= AttackRange)
        {
            agent.isStopped = true;
            if(u.CurrentCombo == 3)
            {
                //Special Attack
                u.weapon.abilities[1].Fire(u.gameObject.GetInstanceID(), target);
                u.ApplyCombo(-3);
            }
            else
            {
                u.weapon.abilities[0].Fire(u.gameObject.GetInstanceID(), target);
                u.ApplyCombo(1);
            }
        }
    }
}
