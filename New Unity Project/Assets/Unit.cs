using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AbstractEffect
{
    public Unit.StatType type;
    public float Strength;
    public bool UseAsFlat = false;
    public float Duration;
    public float TimeActivated;
    public bool active
    {
        get
        {
            return Time.realtimeSinceStartup < TimeActivated + Duration;
        }
    }
    public virtual float Apply(float value)
    {
        return UseAsFlat ? value+Strength:value*Strength;
    }
}

public class Unit : MonoBehaviour {

    public static Dictionary<int, Unit> ActiveUnits = new Dictionary<int, Unit>();

    List<AbstractEffect> effects = new List<AbstractEffect>();
    public Weapon weapon;
    public Animation UnitAnimation;
    public Vector3 vDirNorm;
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
            float ret = _unitHealth;
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

    public float MoveSpeedMultiplicator
    {
        get
        {
            float ret = _moveSpeedMultiplicator;
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

    public void ApplyDamage(float value)
    {
        _unitHealth -= value;
    }

    public void ApplyCombo(float change)
    {
        _currentCombo += change;
    }

    public delegate void FireAnimationEvent();
    public FireAnimationEvent eventListener;
    public void ReceiveAnimationEvent()
    {
        if(eventListener != null)eventListener();
    }

    void RemoveInactive()
    {
        List<int> inactive = new List<int>();
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].active)
            {
                inactive.Add(i);
                
            }
        }
        foreach (int i in inactive)
        {
            effects.RemoveAt(i);
        }
    }

    [SerializeField]
    float _unitHealth;

    [SerializeField]
    float _comboLimit;

    float _currentCombo;

    [SerializeField]
    float _moveSpeedMultiplicator;
    bool _stun = false;

    public enum StatType
    {
        HP = 1,
        COMBO = 2,
        MOVESPEED = 4,
        STUN = 8
    }


    public void ApplyEffects(List<AbstractEffect> effects)
    {
        this.effects.AddRange(effects);
    }

    private void Awake()
    {
        ActiveUnits.Add(gameObject.GetInstanceID(), this);
    }

    // Use this for initialization
    void Start () {
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = gameObject.GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
        RemoveInactive();
	}

    private void OnDestroy()
    {
        ActiveUnits.Remove(gameObject.GetInstanceID());
    }
}
