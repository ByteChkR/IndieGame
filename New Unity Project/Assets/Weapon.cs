using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Weapon : MonoBehaviour {

    public int owner;
    public List<AbstractAbility> abilities;
    public List<KeyCode> abilityKeyBindings;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < abilityKeyBindings.Count; i++)
        {
            if (Input.GetKeyDown(abilityKeyBindings[i]))
            {
                abilities[i].Fire(owner);
            }
        }
	}
}
