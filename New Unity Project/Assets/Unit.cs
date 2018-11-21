using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]
public class AbstractEffect
{
    public Unit.StatType type;
    public float Strength;
    public bool UseAsFlat = false;
    public bool InverseOnTimeout = false;
    public float Duration;
    public float TimeActivated;
    public bool ApplyOnce = true;
    bool appliedOnce = false;
    bool inversed = false;
    public bool active
    {
        get
        {
            return Time.realtimeSinceStartup < TimeActivated + Duration;
        }
    }
    public virtual float Apply(float value)
    {
        if (ApplyOnce && appliedOnce) return value;
        appliedOnce = true;
        return UseAsFlat ? value + Strength : value * Strength;

    }

    public float Inverse(float value)
    {
        if (inversed) return value;
        inversed = true;
        return UseAsFlat ? value - Strength : value * 1 / Strength;
    }
}

[System.Serializable]
public class UnitStats
{
    [SerializeField]
    private float MaxHealth;
    [SerializeField]
    private float MaxCombo;
    [SerializeField]
    private float MaxMovementSpeed;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _currentCombo;
    [SerializeField]
    private float _currentMovementSpeed;
    [SerializeField]
    private bool _stun = false;
    public float CurrentHealth { get { return _currentHealth; } }
    public float CurrentCombo { get { return _currentCombo; } }
    public float CurrentMovementSpeed { get { return _currentMovementSpeed; } }
    public bool IsStunned { get { return _stun; } }
    private List<AbstractEffect> effects = new List<AbstractEffect>();


    public void AddEffect(AbstractEffect newEffect)
    {
        newEffect.TimeActivated = Time.realtimeSinceStartup;
        effects.Add(newEffect);
    }

    public void AddEffects(AbstractEffect[] newEffects)
    {
        foreach (AbstractEffect effect in newEffects)
        {
            AddEffect(effect);
        }
    }

    public void ApplyValue(Unit.StatType stype, float value)
    {
        Debug.Log(System.Enum.GetName(typeof(Unit.StatType), stype) + ": changed by " + value);
        switch (stype)
        {
            case Unit.StatType.HP:
                _currentHealth += value;
                break;
            case Unit.StatType.COMBO:
                _currentCombo += value;
                break;
            case Unit.StatType.MOVESPEED:
                _currentMovementSpeed += value;
                break;
            case Unit.StatType.STUN:
                _stun = value > 0;
                break;
            default:
                break;
        }
    }

    public void Process()
    {
        List<int> inactiveEffects = new List<int>();
        for (int i = 0; i < effects.Count; i++)
        {
            if (!effects[i].active)
            {

                inactiveEffects.Add(i);


            }

            switch (effects[i].type)
            {
                case Unit.StatType.HP:
                    if (!effects[i].active && effects[i].InverseOnTimeout)
                    {
                        _currentHealth = effects[i].Inverse(_currentHealth);
                    }
                    else if (effects[i].active) _currentHealth = effects[i].Apply(_currentHealth);

                    break;
                case Unit.StatType.COMBO:
                    if (!effects[i].active && effects[i].InverseOnTimeout)
                    {
                        _currentCombo = effects[i].Inverse(_currentCombo);
                    }
                    else if (effects[i].active) _currentCombo = effects[i].Apply(_currentCombo);

                    break;
                case Unit.StatType.MOVESPEED:
                    if (!effects[i].active && effects[i].InverseOnTimeout)
                    {
                        _currentMovementSpeed = effects[i].Inverse(_currentMovementSpeed);
                    }
                    else if (effects[i].active)
                    {
                        _currentMovementSpeed = effects[i].Apply(_currentMovementSpeed);
                    }
                    break;
                case Unit.StatType.STUN:
                    if (!effects[i].active && effects[i].InverseOnTimeout)
                    {
                        _stun = false;
                    }
                    else if (effects[i].active)
                    {
                        _stun = true;
                    }
                    break;
                default:
                    break;
            }
        }
        for (int i = inactiveEffects.Count - 1; i > 0; i--)
        {
            effects.RemoveAt(inactiveEffects[i]);
        }


    }
}

[RequireComponent(typeof(IController))]
public class Unit : MonoBehaviour
{
    public IController controller;
    public static Dictionary<int, Unit> ActiveUnits = new Dictionary<int, Unit>();
    public UnitStats stats;
    private Weapon[] weapons = new Weapon[2];
    public Animation UnitAnimation;
    public NavMeshAgent agent;
    int selectedWeapon = 0;
    public Weapon SelectedWeapon { get { return weapons[selectedWeapon]; } }
    public enum TriggerType { CollisionCheck, Teleport };

    public delegate void AnimationTrigger(TriggerType ttype);
    AnimationTrigger _trigger;

    public void SwitchWeapon()
    {
        if (SelectedWeapon == weapons[0] && weapons[1] != null)
        {
            selectedWeapon = 1;
        }
        else if(weapons[0] != null)
        {
            selectedWeapon = 0;
        }
    }

    void FireAnimationTrigger(TriggerType ttype)
    {
        if (null != _trigger)
        {
            _trigger(ttype);
        }
    }

    public void AddAnimationTriggerListener(AnimationTrigger pTrigger)
    {
        _trigger += pTrigger;
    }

    public void RemoveAnimationTriggerListener(AnimationTrigger pTrigger)
    {
        _trigger -= pTrigger;
    }

    public enum StatType
    {
        HP = 1,
        COMBO = 2,
        MOVESPEED = 4,
        STUN = 8
    }

    private void Awake()
    {
        ActiveUnits.Add(gameObject.GetInstanceID(), this);
    }

    // Use this for initialization
    void Start()
    {
        weapons[0] = GetComponentInChildren<Weapon>();
        weapons[0].SetOwnerDUs(this);
        controller = GetComponent<IController>();
    }

    private void FixedUpdate()
    {
        stats.Process();
    }

    private void OnDestroy()
    {
        ActiveUnits.Remove(gameObject.GetInstanceID());
    }
}
