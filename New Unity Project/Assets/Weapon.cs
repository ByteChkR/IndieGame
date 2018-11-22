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

    // Use this for initialization
    void Start()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void SetOwnerDUs(Unit pOwner)
    {
        oOwner = pOwner;
        owner = pOwner.gameObject.GetInstanceID();
    }

    public void SetOwner(int unitId)
    {
        owner = unitId;
        if(unitId >= 0) oOwner = Unit.ActiveUnits[owner];
    }
    
    private void PreparePickup()
    {
        if (owner < 0)
        { 
            coll.size = new Vector3(3, 3, 3);
            rb.useGravity = true;
        }
        else
        { 
            coll.size = new Vector3(1, 1, 1);
            rb.useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (oOwner == null || owner == -1) return;
        for (int i = 0; i < abilityKeyBindings.Count; i++)
        {
            if (!oOwner.stats.IsStunned && Input.GetKey(abilityKeyBindings[i]))
            {
                Debug.Log(abilities[i].Name + " : " + oOwner.controller.VTarget);
                abilities[i].Fire(owner, oOwner.controller.VTarget);
            }
        }
    }

}
