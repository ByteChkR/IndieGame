using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class AbstractAbilityInstance : MonoBehaviour
{
    Dictionary<int, bool> players = new Dictionary<int, bool>();
    Unit source;
    protected List<int> unitIDHit = new List<int>();
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

    public virtual void OnHit(int id)
    {

    }

    private void Update()
    {
        List<int> unitHit = Physics.OverlapBox(source.weapon.transform.position, source.weapon.coll.size / 2, source.weapon.transform.rotation).Select(x => x.GetInstanceID()).ToList();
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

    public virtual void Fire(int dummy)
    {
        AbstractAbilityInstance a = GameObject.Instantiate(abilityInstance);
        a.RegisterSource(dummy);
    }



}
