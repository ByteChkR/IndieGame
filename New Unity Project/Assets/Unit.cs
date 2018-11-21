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
        return UseAsFlat ? value + Strength : value * Strength;
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
                continue;
            }
            
            switch (effects[i].type)
            {
                case Unit.StatType.HP:
                    _currentHealth = effects[i].Apply(_currentHealth);
                    break;
                case Unit.StatType.COMBO:
                    _currentCombo = effects[i].Apply(_currentCombo);
                    break;
                case Unit.StatType.MOVESPEED:
                    _currentMovementSpeed = effects[i].Apply(_currentMovementSpeed);
                    break;
                case Unit.StatType.STUN:
                    _stun = true;
                    break;
                default:
                    break;
            }
        }
        for (int i = inactiveEffects.Count; i > 0; i--)
        {
            effects.RemoveAt(i);
        }


    }
}

[RequireComponent(typeof(IController))]
public class Unit : MonoBehaviour
{
    public IController controller;
    public static Dictionary<int, Unit> ActiveUnits = new Dictionary<int, Unit>();
    public UnitStats stats;
    public Weapon weapon;
    public Animation UnitAnimation;
    public Vector3 vDirNorm;
    public NavMeshAgent agent;


    public delegate void FireAnimationEvent();
    public FireAnimationEvent eventListener;
    public void ReceiveAnimationEvent()
    {
        if (eventListener != null) eventListener();
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
        weapon = GetComponentInChildren<Weapon>();
        weapon.owner = gameObject.GetInstanceID();
        controller = GetComponent<IController>();
    }

    // Update is called once per frame
    void Update()
    {

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
