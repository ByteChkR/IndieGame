using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    public Vector3 VTarget { get { return target.position; } }
    public Transform target;
    public NavMeshAgent agent;
    public float AttackRange;
    public float ActivationRange;
    float distance2Target;
    Unit u;
    public float Speed = 3.5f;
    bool CanSeeTarget
    {
        get
        {
            RaycastHit info;
            bool hit = Physics.Raycast(transform.position + (target.position - transform.position).normalized, target.position - transform.position, out info, float.MaxValue) && info.collider.gameObject.GetInstanceID() == target.gameObject.GetInstanceID();
            Debug.DrawRay(transform.position + (target.position - transform.position).normalized, target.position - transform.position);
            distance2Target = info.distance;
            return hit && distance2Target <= ActivationRange;
        }
    }
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        u = Unit.ActiveUnits[gameObject.GetInstanceID()];
        u.agent = agent;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = u.stats.CurrentMovementSpeed * Speed;
        if (!CanSeeTarget)
        {
            return;
        }

        if (u.stats.IsStunned)
        {
            u.agent.isStopped = true;
            u.agent.velocity = Vector3.zero;
            return;
        }
        if (distance2Target <= ActivationRange)
        {
            if (distance2Target > AttackRange)
            {
                agent.SetDestination(target.position);
                agent.isStopped = false;
            }
            else
            {

                agent.isStopped = true;
                if (!u.UnitAnimation.isPlaying)
                {

                    transform.forward = target.position - transform.position;
                    if (u.stats.CurrentCombo >= u.SelectedWeapon.abilities[1].ComboCost)
                    {
                        //Special Attack
                        u.SelectedWeapon.abilities[1].Fire(u.gameObject.GetInstanceID(), target.position);

                    }
                    else
                    {
                        u.SelectedWeapon.abilities[0].Fire(u.gameObject.GetInstanceID(), target.position);

                    }
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }

    }
}
