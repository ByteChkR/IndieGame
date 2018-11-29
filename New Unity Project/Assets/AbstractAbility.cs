using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AbstractAbility
{
    public bool IsSpecial = false;
    public string Name;
    public string Description;
    public Sprite icon;
    public Ability abilityInstance;
    public float ComboCost;
    public float cooldown;
    public Unit.AnimationStates animState;
    public bool OnCooldown { get { return lastTimeUsed >= Time.realtimeSinceStartup - cooldown; } }
    float lastTimeUsed = float.MinValue;
    public virtual bool Fire(int dummy, Vector3 target , Quaternion rot)
    {
        if (!OnCooldown && Unit.ActiveUnits[dummy].Stats.CurrentCombo >= ComboCost)
        {
            Unit.ActiveUnits[dummy].Stats.ApplyValue(Unit.StatType.COMBO, -ComboCost, -1, false);
            Ability a = GameObject.Instantiate(abilityInstance, Unit.ActiveUnits[dummy].transform.position, Unit.ActiveUnits[dummy].transform.rotation);
            a.SetAnimState(animState);
            a.Initialize(dummy, target, rot, IsSpecial);

            lastTimeUsed = Time.realtimeSinceStartup;
            return true;
        }
        return false;

    }



}
