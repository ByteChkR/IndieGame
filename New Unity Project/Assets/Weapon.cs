using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[RequireComponent(typeof(BoxCollider))]
public class Weapon : MonoBehaviour
{

    public int Owner = -1;
    public Unit OOwner;
    public List<AbstractAbility> Abilities;
    public List<KeyCode> AbilityKeyBindings;
    public BoxCollider Coll;
    public Rigidbody Rb;
    public WeaponInfoScript WeaponIS;
    public bool IsOnGround { get { return Owner == gameObject.GetInstanceID(); } }
    private bool _wasSetPreInit = false;
    private bool _wasSetOnce = false;
    public bool UseMultipleAttacks = false;
    public int AttackCount = 1;
    int cur = 0;
    public bool TurnTowardsTarget = false;

    public int GoldValue = 5;
    // Use this for initialization
    void Start()
    {

        Rb = GetComponent<Rigidbody>();
        Coll = GetComponent<BoxCollider>();
        if (!_wasSetPreInit && !_wasSetOnce)
        {
            Owner = gameObject.GetInstanceID();
        }
        PreparePickup();
        WeaponIS.Weapon = gameObject;
    }

    public void DisableBuying()
    {
        WeaponIS.SetCost(0);
    }

    public void ActivateInfoBox()
    {
        WeaponIS.gameObject.SetActive(true);
    }

    public void DeactivateInfoBox()
    {
        WeaponIS.gameObject.SetActive(false);
    }

    public void SetOwnerDUs(Unit pOwner)
    {
        OOwner = pOwner;
        Owner = pOwner.gameObject.GetInstanceID();
        PreparePickup();
    }

    public void SetOwnerForgetUnit(int unitId)
    {
        Owner = unitId;
        OOwner = null;
        PreparePickup();
    }

    private void PreparePickup()
    {
        _wasSetOnce = true;
        if (Coll == null)
        {
            _wasSetPreInit = true;
            return;
        }
        if (Owner == gameObject.GetInstanceID())
        {
            Coll.isTrigger = false;
            Rb.useGravity = true;
            Rb.isKinematic = false;
        }
        else
        {
            Coll.isTrigger = true;
            Rb.useGravity = false;
            Rb.isKinematic = true;
        }
    }




    // Update is called once per frame
    void Update()
    {

        //PreparePickup();
        if (OOwner == null || Owner == gameObject.GetInstanceID()) return;
        for (int i = 0; i < AbilityKeyBindings.Count; i++)
        {
            if (!OOwner.Stats.IsStunned && Input.GetKey(AbilityKeyBindings[i]) && OOwner.GetActiveWeapon() == this)
            {
                if (Abilities[i].IsAvailable(Owner))
                {
                    
                    
                    if (TurnTowardsTarget)
                    {
                        Vector3 targetDir = OOwner.Controller.ViewingDirection(true);
                        targetDir.Set(targetDir.x, 0, targetDir.z);
                        OOwner.transform.forward = targetDir;
                    }
                    if (Abilities[i].Fire(Owner, OOwner.Controller.VTarget, OOwner.transform.rotation))
                    {
                        if (UseMultipleAttacks && Abilities[i].animState == Unit.AnimationStates.ATTACK)
                        {
                            OOwner.UnitAnimation.SetInteger("attack", cur);
                            cur++;
                            cur = cur % AttackCount;

                        }

                        OOwner.SetAnimationState(Abilities[i].animState);
                    } 
                }
                //Debug.Log(Abilities[i].Name + " : " + OOwner.Controller.VTarget);
            }
        }
    }
}


