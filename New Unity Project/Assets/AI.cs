using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour, IController
{
    public Vector3 VTarget { get { return Target.position; } }
    public Transform Target;
    public NavMeshAgent Agent;
    public float AttackRange;
    public float ActivationRange;
    float _distance2Target;
    Unit _unit;
    public bool IsPlayer { get { return false; } }
    public float Speed = 3.5f;
    bool _lockControls = false;
    Rigidbody _rb;
    public Rigidbody Rb { get { return _rb; } }
    public void LockControls(bool locked)
    {
        _lockControls = locked;
        Agent.velocity = Vector3.zero;
    }
    bool _canSeeTarget
    {
        get
        {
            if (Target == null) return false;
            RaycastHit info;
            bool hit = Physics.Raycast(transform.position + (Target.position - transform.position).normalized, Target.position - transform.position, out info, float.MaxValue) && info.collider.gameObject.GetInstanceID() == Target.gameObject.GetInstanceID();
            //Debug.DrawRay(transform.position + (Target.position - transform.position).normalized, Target.position - transform.position);
            _distance2Target = info.distance;
            return hit && _distance2Target <= ActivationRange;
        }
    }
    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        _unit = Unit.ActiveUnits[gameObject.GetInstanceID()];
        _unit.Agent = Agent;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Agent.speed = _unit.Stats.CurrentMovementSpeed * Speed;
        if (!_canSeeTarget || _lockControls)
        {
            return;
        }

        if (_unit.Stats.IsStunned)
        {
            _unit.Agent.isStopped = true;
            _unit.Agent.velocity = Vector3.zero;
            return;
        }
        if (_distance2Target <= ActivationRange)
        {
            if (_distance2Target > AttackRange)
            {
                Agent.SetDestination(Target.position);
                Agent.isStopped = false;
            }
            else
            {

                Agent.isStopped = true;
                if (!_unit.UnitAnimation.isPlaying)
                {

                    transform.forward = Target.position - transform.position;
                    if (_unit.SelectedWeapon.Abilities.Count > 1 && _unit.Stats.CurrentCombo >= _unit.SelectedWeapon.Abilities[1].ComboCost)
                    {
                        //Special Attack
                        _unit.SelectedWeapon.Abilities[1].Fire(_unit.gameObject.GetInstanceID(), Target.position, Target.rotation);

                    }
                    else
                    {
                        _unit.SelectedWeapon.Abilities[0].Fire(_unit.gameObject.GetInstanceID(), Target.position, Target.rotation);

                    }
                }
            }
        }
        else
        {
            Agent.isStopped = true;
        }

    }
}
