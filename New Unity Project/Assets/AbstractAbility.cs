using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class AbstractAbilityInstance : MonoBehaviour
{
    Dictionary<int, bool> players = new Dictionary<int, bool>();
    protected Unit source;
    protected List<int> unitIDHit = new List<int>();
    BoxCollider specialCol;
    public Transform target;
    bool HasSpecialCollider = false;
    private BoxCollider Hitbox
    {
        get
        { 
            return HasSpecialCollider ? specialCol : source.SelectedWeapon.coll;
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
    public Ability abilityInstance;
    public float ComboCost;
    public float cooldown;
    public bool OnCooldown { get { return lastTimeUsed >= Time.realtimeSinceStartup - cooldown; } }
    float lastTimeUsed = float.MinValue;
    public virtual bool Fire(int dummy, Vector3 target = new Vector3())
    {
        if (!OnCooldown && Unit.ActiveUnits[dummy].stats.CurrentCombo >= ComboCost)
        {
            Unit.ActiveUnits[dummy].stats.ApplyValue(Unit.StatType.COMBO, -ComboCost);
            Ability a = GameObject.Instantiate(abilityInstance, Unit.ActiveUnits[dummy].transform.position, Unit.ActiveUnits[dummy].transform.rotation);
            a.Initialize(dummy, target);
            lastTimeUsed = Time.realtimeSinceStartup;
            return true;
        }
        return false;

    }



}
