using UnityEngine;
using System.Linq;
using System.Collections.Generic;
[System.Serializable]
public class UnitStats
{
    [SerializeField, Tooltip("Spawn Health")]
    private float _baseHealth;
    [SerializeField, Tooltip("Spawn MovementSpeed")]
    private float _baseMovementSpeed;
    [SerializeField, Tooltip("Spawn Combo")]
    private float _baseCombo;


    private int _killer = -1;
    /// <summary>
    /// The Unit ID that had the killing blow.
    /// </summary>
    public int Killer { get { return _killer; } }


    [SerializeField]
    public float MaxHealth;
    [SerializeField]
    public float MaxCombo;
    [SerializeField]
    public float MaxMovementSpeed;
    private float _currentHealth;
    private float _currentCombo;
    private float _currentMovementSpeed;
    private bool _stun = false;
    private float _currentGold;

    public bool OnlyDamagedWithSpecialAttacks = false;
    public float CurrentHealth { get { return _currentHealth; } }
    public float CurrentCombo { get { return _currentCombo; } }
    public float CurrentMovementSpeed { get { return _currentMovementSpeed; } }
    public bool IsStunned { get { return _stun; } }
    public float CurrentGold { get { return _currentGold; } }
    private List<KeyValuePair<int, AbstractEffect>> _effects = new List<KeyValuePair<int, AbstractEffect>>();
    

    public void Init()
    {
        _baseCombo = 0;
        _baseMovementSpeed = 1;
        _baseHealth = MaxHealth;
        _currentMovementSpeed = _baseMovementSpeed;
        _currentHealth = _baseHealth;
        _currentCombo = _baseCombo;
    }

    public void AddEffect(AbstractEffect newEffect, int source)
    {
        newEffect.TimeActivated = Time.realtimeSinceStartup;
        _effects.Add(new KeyValuePair<int, AbstractEffect>(source, newEffect));
    }

    public void AddEffects(AbstractEffect[] newEffects, int source)
    {
        foreach (AbstractEffect effect in newEffects)
        {
            AddEffect(effect, source);
        }
    }

    public void ApplyValue(Unit.StatType stype, float value, int source, bool isSpecialAttack)
    {
        //Debug.Log(System.Enum.GetName(typeof(Unit.StatType), stype) + ": changed by " + value);
        switch (stype)
        {
            case Unit.StatType.HP:
                if (OnlyDamagedWithSpecialAttacks && !isSpecialAttack) //If you try deal damage with a normal attack whilst OnlyDamagedWithSpecialAttacks =true
                {
                    break;
                }
              
                _currentHealth += value;
                _currentHealth = _currentHealth > MaxHealth ? MaxHealth : _currentHealth;
                if (_currentHealth <= 0) _killer = source;
                break;
            case Unit.StatType.COMBO:
                _currentCombo += value;
                _currentCombo = _currentCombo > MaxCombo ? MaxCombo : _currentCombo;
                break;
            case Unit.StatType.MOVESPEED:
                _currentMovementSpeed += value;
                _currentMovementSpeed = _currentMovementSpeed > MaxMovementSpeed ? MaxMovementSpeed : _currentMovementSpeed;
                break;
            case Unit.StatType.STUN:
                _stun = value > 0;
                break;
            case Unit.StatType.GOLD:
                _currentGold += value;
                break;
            default:
                break;
        }
    }

    public void Process()
    {
        List<int> inactiveEffects = new List<int>();
        foreach (KeyValuePair<int, AbstractEffect> kvp in _effects)
        {

            if (!kvp.Value.Active)
            {

                inactiveEffects.Add(_effects.IndexOf(kvp));


            }

            switch (kvp.Value.Type)
            {
                case Unit.StatType.HP:
                    if (!kvp.Value.Active && kvp.Value.InverseOnTimeout)
                    {
                        _currentHealth = kvp.Value.Inverse(_currentHealth);
                    }
                    else if (kvp.Value.Active) _currentHealth = kvp.Value.Apply(_currentHealth);
                    if (_currentHealth >= 0) _killer = kvp.Key;
                    break;
                case Unit.StatType.COMBO:
                    if (!kvp.Value.Active && kvp.Value.InverseOnTimeout)
                    {
                        _currentCombo = kvp.Value.Inverse(_currentCombo);
                    }
                    else if (kvp.Value.Active) _currentCombo = kvp.Value.Apply(_currentCombo);

                    break;
                case Unit.StatType.MOVESPEED:
                    if (!kvp.Value.Active && kvp.Value.InverseOnTimeout)
                    {
                        _currentMovementSpeed = kvp.Value.Inverse(_currentMovementSpeed);
                    }
                    else if (kvp.Value.Active)
                    {
                        _currentMovementSpeed = kvp.Value.Apply(_currentMovementSpeed);
                    }
                    break;
                case Unit.StatType.STUN:
                    if (!kvp.Value.Active && kvp.Value.InverseOnTimeout)
                    {
                        _stun = false;
                    }
                    else if (kvp.Value.Active)
                    {
                        _stun = true;
                    }
                    break;
                default:
                    break;
            }
        }
        for (int i = inactiveEffects.Count - 1; i >= 0; i--)
        {
            _effects.RemoveAt(inactiveEffects[i]);
        }


    }
}

[System.Serializable]
public class ParticleSystemEntry
{
    public string Key;
    public ParticleSystem Value;
    public ParticleSystemEntry(string key, ParticleSystem ps)
    {
        Key = key;
        Value = ps;
    }
}
