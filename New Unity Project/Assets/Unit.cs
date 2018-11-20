using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class AbstractEffect
{
    public Unit.StatType type;
    public float Duration;
    public float timeActivated;
    public bool active
    {
        get
        {
            return Time.realtimeSinceStartup < timeActivated + Duration;
        }
    }
    public virtual float Apply(float value)
    {
        return value;
    }
}

public class Unit : MonoBehaviour {

    List<AbstractEffect> effects = new List<AbstractEffect>();

    public bool Stunned
    {
        get
        {
            bool ret = false;
            for (int i = 0; i < effects.Count; i++)
            {
                if(effects[i].type == StatType.STUN)
                {
                    ret = effects[i].Apply(0) > 0;
                }
            }
            return ret;
        }
    }

    public float Health
    {
        get
        {
            float ret = UnitHealth;
            for (int i = 0; i < effects.Count; i++)
            {
                if(effects[i].type == StatType.HP)
                {
                    if (effects[i].active)
                    {
                        ret = effects[i].Apply(ret);
                    }
                }
            }
            return ret;
        }
    }

    public float CurrentCombo
    {
        get
        {
            float ret = _currentCombo;
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].type == StatType.COMBO)
                {
                    if (effects[i].active)
                    {
                        ret = effects[i].Apply(ret);
                    }
                }
            }
            return ret;
        }
    }

    public float MoveSpeed
    {
        get
        {
            float ret = MovementSpeed;
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].type == StatType.MOVESPEED)
                {
                    if (effects[i].active)
                    {
                        ret = effects[i].Apply(ret);
                    }
                }
            }
            return ret;
        }
    }
    

    void RemoveInactive()
    {
        List<int> inactive = new List<int>();
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].active) inactive.Add(i);
        }
        foreach (int i in inactive)
        {
            effects.RemoveAt(i);
        }
    }

    public float UnitHealth;
    public float ComboLimit;
    float _currentCombo;
    public float MovementSpeed;
    bool _stun = false;

    public enum StatType
    {
        HP = 1,
        COMBO = 2,
        MOVESPEED = 4,
        STUN = 8
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RemoveInactive();
	}
}
