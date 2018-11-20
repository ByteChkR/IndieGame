﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class AbstractAbilityInstance : MonoBehaviour
{
    Dictionary<int, bool> players = new Dictionary<int, bool>();
    protected Unit source;
    protected List<int> unitIDHit = new List<int>();
    BoxCollider specialCol;
    bool HasSpecialCollider = false;
    private BoxCollider Hitbox
    {
        get
        {
            return HasSpecialCollider ? specialCol : source.weapon.coll;
        }
    }
    public virtual void RegisterSource(int dummy)
    {
        source = Unit.ActiveUnits[dummy];
    }

    public void RegisterPlayer(int dummy)
    {
        if (players.ContainsKey(dummy)) players[dummy] = true;
        else players.Add(dummy, true);
    }

    public void UnregisterPlayer(int dummy)
    {
        players[dummy] = false;
    }

    public void SetSpecialCollider(BoxCollider collider)
    {
        specialCol = collider;
        HasSpecialCollider = true;
    }

    public virtual void OnHit(int id)
    {

    }

    private void Update()
    {
        List<int> unitHit = Physics.OverlapBox(Hitbox.center, Hitbox.size / 2, Hitbox.transform.rotation).Select(x => x.GetInstanceID()).ToList();
        foreach (int hit in unitHit)
        {
            if (unitIDHit.Contains(hit)) continue;
            unitIDHit.Add(hit);
            OnHit(hit);
        }
    }
}

[System.Serializable]
public class AbstractAbility
{

    public string Name;
    public string Description;
    public Sprite icon;
    public AbstractAbilityInstance abilityInstance;
    public float cooldown;
    float lastTimeUsed = float.MinValue;
    public virtual void Fire(int dummy)
    {
        if (lastTimeUsed <= Time.realtimeSinceStartup - cooldown)
        {
            AbstractAbilityInstance a = GameObject.Instantiate(abilityInstance);
            a.RegisterSource(dummy);
            lastTimeUsed = Time.realtimeSinceStartup;
        }
        
    }



}
