using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractAbilityInstance : MonoBehaviour
{
    Dictionary<int, bool> players = new Dictionary<int, bool>();
    int source;

    public void RegisterSource(int dummy)
    {
        source = dummy;
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
}

public abstract class AbstractAbility : MonoBehaviour
{

    public string Name;
    public string Description;
    public Sprite icon;
    public AbstractAbilityInstance abilityInstance;

    public virtual void Fire(int dummy)
    {
        AbstractAbilityInstance a = Instantiate(abilityInstance);
        a.RegisterSource(dummy);
    }

}
