using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;



[RequireComponent(typeof(IController))]
public class Unit : MonoBehaviour
{
    public IController Controller;
    public bool IsPlayer { get { return Controller.IsPlayer; } }
    public static Dictionary<int, Unit> ActiveUnits = new Dictionary<int, Unit>();
    public static Unit Player;
    public UnitStats Stats;
    private Weapon[] _weapons = new Weapon[2];
    public Animation UnitAnimation;
    public NavMeshAgent Agent;
    [SerializeField]
    public List<ParticleSystemEntry> ParticleSystems;
    public CheckpointScript Checkpoint;
    public int GoldReward = 2;
    public GameObject GoldPrefab;
    public Rigidbody rb;
    private int _selectedWeapon = 0;
    public Weapon SelectedWeapon { get { return _weapons[_selectedWeapon]; } }
    public enum TriggerType
    {
        CollisionCheck,
        Teleport,
        ControlLock,
        ControlUnlock,

    };

    public delegate void AnimationTrigger(TriggerType ttype);
    private AnimationTrigger _trigger;


    public Weapon GetActiveWeapon()
    {
        return _weapons[_selectedWeapon];
    }

    void OnTriggerEnter(Collider coll)
    {
        CheckpointScript cp;
        if (IsPlayer && null != (cp = coll.GetComponent<CheckpointScript>()))
        {
            if (cp.Index > Checkpoint.Index || Checkpoint == null)
            {
                cp.TakeCheckpoint(Stats.CurrentGold, transform.position, _weapons);
                Checkpoint = cp;
            }
        }
    }

    void OnCollisionStay(Collision coll)
    {
        Weapon w = null;
        if (IsPlayer && null != (w = (coll.collider.GetComponent<Weapon>())))
        {
            w.ActivateInfoBox();
            if (Input.GetKeyDown(KeyCode.E) && w.IsOnGround && w.GoldValue <= Stats.CurrentGold)
            {
                Stats.ApplyValue(StatType.GOLD, -w.GoldValue);
                PickupWeapon(w);
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        Weapon w = null;
        if (IsPlayer && null != (w = (coll.collider.GetComponent<Weapon>())))
        {
            w.DeactivateInfoBox();
        }
    }


    public void PickupWeapon(Weapon pWeapon)
    {
        pWeapon.DisableBuying();
        Debug.Log(pWeapon.Owner);



        pWeapon.SetOwnerDUs(this);
        pWeapon.transform.parent = _weapons[_selectedWeapon].transform.parent;
        pWeapon.transform.position = _weapons[_selectedWeapon].transform.position;
        pWeapon.transform.rotation = _weapons[_selectedWeapon].transform.rotation;
        pWeapon.transform.localScale = _weapons[_selectedWeapon].transform.localScale;

        if (_weapons[1] == null)
        {
            _weapons[1] = pWeapon;
            SwitchWeapon(); //Switch to new weapon
        }
        else
        {
            DropWeapon();
            _weapons[_selectedWeapon] = pWeapon;

        }

    }

    public void DropWeapon()
    {
        _weapons[_selectedWeapon].transform.parent = null;
        _weapons[_selectedWeapon].SetOwnerForgetUnit(_weapons[_selectedWeapon].gameObject.GetInstanceID());
        _weapons[_selectedWeapon] = null;
    }


    public void SwitchWeapon()
    {
        int last = _selectedWeapon;
        if (_weapons[1] != null && _selectedWeapon == 0)
        {
            _selectedWeapon = 1;
        }
        else if (_weapons[0] != null)
        {
            _selectedWeapon = 0;
        }
        _weapons[last].gameObject.SetActive(false);
        _weapons[_selectedWeapon].gameObject.SetActive(true);
        Debug.Log("selected weapon: " + _selectedWeapon);
    }

    void RegisterParticleEffect(string key, ParticleSystem ps)
    {
        if (ParticleSystems.Count(x => x.Key == key) > 0)
        {
            Debug.LogError("Tried to register particle system with key that is already existing");
            return;
        }
        ParticleSystems.Add(new ParticleSystemEntry(key, ps));

    }

    void UnRegisterParticleEffect(string key)
    {
        if (ParticleSystems.Count(x => x.Key == key) == 0) return;
        ParticleSystems.Remove(ParticleSystems.First(x => x.Key == key));
    }

    void TriggerParticleEffect(string key)
    {
        foreach (ParticleSystemEntry ps in ParticleSystems)
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

    public void FireAnimationTrigger(TriggerType ttype)
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
        Controller.LockControls(locked);
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
        Physics.IgnoreLayerCollision(11, 11);
        Stats.Init();
        ActiveUnits.Add(gameObject.GetInstanceID(), this);

    }

    // Use this for initialization
    void Start()
    {
        _weapons[0] = GetComponentInChildren<Weapon>();
        _weapons[0].SetOwnerDUs(this);

        rb = GetComponent<Rigidbody>();

        Controller = GetComponent<IController>();
        if (IsPlayer) Player = this;
    }

    private void FixedUpdate()
    {
        Stats.Process();
    }

    private void UnitDying()
    {
        Vector3 rnd = new Vector3();
        Vector2 r;
        for (int i = 0; i < GoldReward; i++)
        {
            r = Random.insideUnitCircle * 2;
            rnd.Set(r.x, 0, r.y);
            Coin a = Instantiate(GoldPrefab, transform.position + rnd, transform.rotation).GetComponent<Coin>();
            a.Target = Unit.Player;
            a.Initialize(Stats.Killer, Vector3.zero, Quaternion.identity); //use the source int as the killers id. This works only with coins.
            
        }
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (Stats.CurrentHealth <= 0) UnitDying();
    }

    private void OnDestroy()
    {
        ActiveUnits.Remove(gameObject.GetInstanceID());
    }
}
