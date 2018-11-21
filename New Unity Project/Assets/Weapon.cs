using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(BoxCollider))]
public class Weapon : MonoBehaviour {

    public int owner;
    public Unit oOwner;
    public List<AbstractAbility> abilities;
    public List<KeyCode> abilityKeyBindings;
    public BoxCollider coll;

	// Use this for initialization
	void Start () {
        coll = GetComponent<BoxCollider>();
	}
	
    public void SetOwnerDUs(Unit pOwner)
    {
        oOwner = pOwner;
        owner = pOwner.gameObject.GetInstanceID();
    }

    public void SetOwner(int unitId)
    {
        owner = unitId;
        oOwner = Unit.ActiveUnits[owner];
    }

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < abilityKeyBindings.Count; i++)
        {
            if (!oOwner.stats.IsStunned && Input.GetKey(abilityKeyBindings[i]))
            {
                Debug.Log(abilities[i].Name +" : " + oOwner.controller.VTarget);
                abilities[i].Fire(owner, oOwner.controller.VTarget);
            }
        }
	}

}
