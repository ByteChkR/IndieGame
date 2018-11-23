using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
[System.Serializable]
public class AbstractEffect
{
    public Unit.StatType type;
    public float Strength;
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
        return value + Strength;

    }

    public float Inverse(float value)
    {
        if (inversed) return value;
        inversed = true;
        return value - Strength;
    }
}

[System.Serializable]
public class UnitStats
{
    [SerializeField]
    private float BaseHealth;
    [SerializeField]
    private float BaseMovementSpeed;
    [SerializeField]
    private float BaseCombo;





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


    public float CurrentHealth { get { return _currentHealth; } }
    public float CurrentCombo { get { return _currentCombo; } }
    public float CurrentMovementSpeed { get { return _currentMovementSpeed; } }
    public bool IsStunned { get { return _stun; } }
    public float CurrentGold { get { return _currentGold; } }
    private List<AbstractEffect> effects = new List<AbstractEffect>();


    public void Init()
    {
        _currentMovementSpeed = BaseMovementSpeed;
        _currentHealth = BaseHealth;
        _currentCombo = BaseCombo;
    }

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
                _currentHealth = _currentHealth > MaxHealth ? MaxHealth : _currentHealth;
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
        for (int i = inactiveEffects.Count - 1; i >= 0; i--)
        {
            effects.RemoveAt(inactiveEffects[i]);
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

[RequireComponent(typeof(IController))]
public class Unit : MonoBehaviour
{
    public IController controller;
    public bool isPlayer { get { return controller.isPlayer; } }
    public static Dictionary<int, Unit> ActiveUnits = new Dictionary<int, Unit>();
    public static Unit Player;
    public UnitStats stats;
    private Weapon[] weapons = new Weapon[2];
    public Animation UnitAnimation;
    public NavMeshAgent agent;
    [SerializeField]
    public List<ParticleSystemEntry> particleSystems;
    public CheckpointScript checkpoint;

    int selectedWeapon = 0;
    public Weapon SelectedWeapon { get { return weapons[selectedWeapon]; } }
    public enum TriggerType
    {
        CollisionCheck,
        Teleport,
        ControlLock,
        ControlUnlock,

    };

    public delegate void AnimationTrigger(TriggerType ttype);
    AnimationTrigger _trigger;


    public Weapon GetActiveWeapon()
    {
        return weapons[selectedWeapon];
    }

    void OnTriggerEnter(Collider coll)
    {
        CheckpointScript cp;
        if(null != (cp = coll.GetComponent<CheckpointScript>()))
        {
            if(cp.index > checkpoint.index || checkpoint == null)
            {
                cp.TakeCheckpoint(stats.CurrentGold, transform.position,weapons);
                checkpoint = cp;
            }
        }
    }

    void OnCollisionStay(Collision coll)
    {
        Weapon w = null;
        if (null != (w = (coll.collider.GetComponent<Weapon>())))
        {
            if (Input.GetKeyDown(KeyCode.E) && w.isOnGround)
            {
                Debug.Log("pickup");
                Debug.Log("weapon id " + w.GetInstanceID());
                PickupWeapon(w);
            }
        }
    }

    public void PickupWeapon(Weapon pWeapon)
    {

        Debug.Log(pWeapon.owner);



        pWeapon.SetOwnerDUs(this);
        pWeapon.transform.parent = weapons[selectedWeapon].transform.parent;
        pWeapon.transform.position = weapons[selectedWeapon].transform.position;
        pWeapon.transform.rotation = weapons[selectedWeapon].transform.rotation;
        pWeapon.transform.localScale = weapons[selectedWeapon].transform.localScale;

        if (weapons[1] == null)
        {
            weapons[1] = pWeapon;
            SwitchWeapon(); //Switch to new weapon
        }
        else
        {
            DropWeapon();
            weapons[selectedWeapon] = pWeapon;

        }

    }

    public void DropWeapon()
    {
        weapons[selectedWeapon].transform.parent = null;
        weapons[selectedWeapon].SetOwnerForgetUnit(weapons[selectedWeapon].gameObject.GetInstanceID());
        weapons[selectedWeapon] = null;
    }


    public void SwitchWeapon()
    {
        int last = selectedWeapon;
        if (weapons[1] != null && selectedWeapon == 0)
        {
            selectedWeapon = 1;
        }
        else if (weapons[0] != null)
        {
            selectedWeapon = 0;
        }
        weapons[last].gameObject.SetActive(false);
        weapons[selectedWeapon].gameObject.SetActive(true);
        Debug.Log("selected weapon: " + selectedWeapon);
    }

    void RegisterParticleEffect(string key, ParticleSystem ps)
    {
        if(particleSystems.Count(x=>x.Key == key) > 0)
        {
            Debug.LogError("Tried to register particle system with key that is already existing");
            return;
        }
        particleSystems.Add(new ParticleSystemEntry(key, ps));

    }

    void UnRegisterParticleEffect(string key)
    {
        if (particleSystems.Count(x => x.Key == key) == 0) return;
        particleSystems.Remove(particleSystems.First(x => x.Key == key));
    }

    void TriggerParticleEffect(string key)
    {
        foreach (ParticleSystemEntry ps in particleSystems)
        {
            if (ps.Key == key)
            {
                if (ps.Value.isStopped)
                {
                    ps.Value.Play();
                }
                else
                {
                    ps.Value.Stop();
                }
            }
        }
    }


    void TriggerSoundEffect(AudioManager.SoundEffect effect)
    {
        AudioManager.instance.PlaySoundEffect(effect);
    }

    void FireAnimationTrigger(TriggerType ttype)
    {
        if (ttype == TriggerType.ControlLock)
        {
            LockControls(true);
        }
        else if (ttype == TriggerType.ControlUnlock)
        {
            LockControls(false);
        }
        if (null != _trigger)
        {
            _trigger(ttype);
        }
    }

    void LockControls(bool locked)
    {
        controller.LockControls(locked);
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
        STUN = 8,
        GOLD = 16

    }

    private void Awake()
    {
        ActiveUnits.Add(gameObject.GetInstanceID(), this);
        
        stats.Init();
    }

    // Use this for initialization
    void Start()
    {
        weapons[0] = GetComponentInChildren<Weapon>();
        weapons[0].SetOwnerDUs(this);
        
        controller = GetComponent<IController>();
        if (isPlayer) Player = this;
    }

    private void FixedUpdate()
    {
        stats.Process();
    }

    private void LateUpdate()
    {
        if (stats.CurrentHealth <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ActiveUnits.Remove(gameObject.GetInstanceID());
    }
}
