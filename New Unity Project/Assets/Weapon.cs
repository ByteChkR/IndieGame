﻿using System.Collections;
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
    public int ID = 0;
    public Sprite WeaponIcon;
    public List<KeyCode> AbilityKeyBindings
    {
        get
        {
            List<KeyCode> ret = new List<KeyCode>();
            if(OOwner != null && OOwner.UnitController.IsPlayer)
            {
                for (int i = 0; i < Controller.interactions.Length; i++)
                {
                    if(i == (int)Controller.Interactions.ABILITY_ONE || i == (int)Controller.Interactions.ABILITY_TWO)
                    {
                        ret.Add(Controller.interactions[i]);
                    }
                }
            }
            return ret;
        }
    }
    public BoxCollider Coll;
    public Rigidbody Rb;
    public WeaponInfoScript WeaponIS;
    public bool IsOnGround { get { return Owner == gameObject.GetInstanceID(); } }
    private bool _wasSetPreInit = false;
    private bool _wasSetOnce = false;
    public bool UseMultipleAttacks;
    public int AttackCount = 1;
    public int SpecialAttackAnimation = 0;
    int cur = 0;
    public bool TurnTowardsTarget = false;
    private bool Activate = false;

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

    public void SetActive(bool active)
    {
        Activate = active;
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
        if (OOwner == null || Owner == gameObject.GetInstanceID() || !Activate || !OOwner.UnitController.IsPlayer) return;
        for (int i = 0; i < AbilityKeyBindings.Count; i++)
        {
            if (!OOwner.Stats.IsStunned && Input.GetKey(AbilityKeyBindings[i]) && OOwner.GetActiveWeapon() == this)
            {
                if (Abilities[i].IsAvailable(Owner))
                {
                    Vector3 targetDir;
                    if (TurnTowardsTarget)
                    {
                        targetDir = OOwner.UnitController.ViewingDirection(true);
                        targetDir.Set(targetDir.x, 0, targetDir.z);
                        OOwner.transform.forward = targetDir;
                    }
                    else
                    {
                        targetDir = OOwner.UnitController.VTarget;
                    }
                    if (Abilities[i].Fire(Owner, targetDir, OOwner.transform.rotation))
                    {
                        if (UseMultipleAttacks && Abilities[i].animState == Unit.AnimationStates.ATTACK)
                        {
                            OOwner.UnitAnimation.SetInteger("attack", cur);
                            cur++;
                            cur = cur % AttackCount;

                        }
                        if (Abilities[i].animState == Unit.AnimationStates.SPECIAL)
                        {
                            OOwner.UnitAnimation.SetInteger("spattack", SpecialAttackAnimation);
                        }

                        OOwner.SetAnimationState(Abilities[i].animState);
                    } 
                }
                //Debug.Log(Abilities[i].Name + " : " + OOwner.Controller.VTarget);
            }
        }
    }
}


