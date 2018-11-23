using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(BoxCollider))]
public class Weapon : MonoBehaviour
{

    public int owner = -1;
    public Unit oOwner;
    public List<AbstractAbility> abilities;
    public List<KeyCode> abilityKeyBindings;
    public BoxCollider coll;
    public Rigidbody rb;
    public bool isOnGround { get { return owner == gameObject.GetInstanceID(); } }
    bool WasSetPreInit = true;
    // Use this for initialization
    void Start()
    {
        owner = gameObject.GetInstanceID();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        if (WasSetPreInit)
        {
            PreparePickup();
            WasSetPreInit = false;
        }
    }

    public void SetOwnerDUs(Unit pOwner)
    {
        oOwner = pOwner;
        owner = pOwner.gameObject.GetInstanceID();
        PreparePickup();
    }

    public void SetOwnerForgetUnit(int unitId)
    {
        owner = unitId;
        oOwner = null;
        PreparePickup();
    }
    
    private void PreparePickup()
    {
        if (owner == gameObject.GetInstanceID())
        {
            coll.isTrigger = false;
            rb.useGravity = true;
            rb.isKinematic = false;
            coll.size = new Vector3(2, 2, 2);
        }
        else
        {
            coll.isTrigger = true;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //PreparePickup();
        if (oOwner == null || owner == gameObject.GetInstanceID()) return;
        for (int i = 0; i < abilityKeyBindings.Count; i++)
        {
            if (!oOwner.stats.IsStunned && Input.GetKey(abilityKeyBindings[i]) && oOwner.GetActiveWeapon() == this)
            {
                Debug.Log(abilities[i].Name + " : " + oOwner.controller.VTarget);
                abilities[i].Fire(owner, oOwner.controller.VTarget);
            }
        }
    }

}
