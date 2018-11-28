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
    //private Weapon[] _weapons = new Weapon[2];
    private Weapon _weapon;
    //public Animation UnitAnimation;
    public Animator UnitAnimation;
    public NavMeshAgent Agent;
    [SerializeField]
    public List<ParticleSystemEntry> ParticleSystems;
    public CheckpointScript Checkpoint;
    public int GoldReward = 2;
    public GameObject GoldPrefab;
    public Rigidbody rb;
    public GameObject WeaponContainer;
    //private int _selectedWeapon = 0;
    //public Weapon SelectedWeapon { get { return _weapons[_selectedWeapon]; } }
    public enum TriggerType
    {
        CollisionCheck,
        Teleport,
        ControlLock,
        ControlUnlock,
        EndAnimation
    };

    public enum AnimationStates : int
    {
        IDLE = 0,
        WALKING = 1,
        ATTACK = 2,
        STUN = 3,
        DEATH = 4,
        SPECIAL = 5
    }

    public void SetAnimationState(AnimationStates newState)
    {
        UnitAnimation.SetInteger("state", (int)newState);
    }

    public delegate void AnimationTrigger(TriggerType ttype);
    private AnimationTrigger _trigger;


    public Weapon GetActiveWeapon()
    {
        //return _weapons[_selectedWeapon];
        return _weapon;
    }

    void OnTriggerEnter(Collider coll)
    {
        CheckpointScript cp;
        if (IsPlayer && null != (cp = coll.GetComponent<CheckpointScript>()))
        {
            if (cp.Index > Checkpoint.Index || Checkpoint == null)
            {
                //cp.TakeCheckpoint(Stats.CurrentGold, transform.position, _weapons);
                cp.TakeCheckpoint(Stats.CurrentGold, transform.position, _weapon);
                Checkpoint = cp;
            }
        }
    }

    void OnCollisionStay(Collision coll)
    {
        Weapon w = null;

        if (Controller == null)
        {
            return;
        }

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
        if (Controller == null)
        {
            return;
        }

        Weapon w = null;
        if (IsPlayer && null != (w = (coll.collider.GetComponent<Weapon>())))
        {
            w.DeactivateInfoBox();
        }
    }


    public void PickupWeapon(Weapon pWeapon)
    {
        pWeapon.DisableBuying();



        pWeapon.SetOwnerDUs(this);
        /*
        pWeapon.transform.parent = _weapons[_selectedWeapon].transform.parent;
        pWeapon.transform.position = _weapons[_selectedWeapon].transform.position;
        pWeapon.transform.rotation = _weapons[_selectedWeapon].transform.rotation;
        pWeapon.transform.localScale = _weapons[_selectedWeapon].transform.localScale;
        */
        pWeapon.transform.parent = WeaponContainer.transform;
        pWeapon.transform.position = WeaponContainer.transform.position;
        pWeapon.transform.rotation = WeaponContainer.transform.rotation;
        pWeapon.transform.localScale = Vector3.one;
        /*
        if (_weapon == null)
        {
            _weapon = pWeapon;
            SwitchWeapon(); //Switch to new weapon
        }
        */
        DropWeapon();
        //_weapons[_selectedWeapon] = pWeapon;
        _weapon = pWeapon;

    }

    public void DropWeapon()
    {
        if (_weapon == null) return;
        /*
        _weapons[_selectedWeapon].transform.parent = null;
        _weapons[_selectedWeapon].SetOwnerForgetUnit(_weapons[_selectedWeapon].gameObject.GetInstanceID());
        _weapons[_selectedWeapon] = null;
        */
        _weapon.transform.parent = null;
        _weapon.SetOwnerForgetUnit(_weapon.gameObject.GetInstanceID());
        _weapon = null;
    }

    /*
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
    */
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

    public void FireAnimationTrigger(int ttype)
    {
        FireAnimationTrigger((TriggerType)ttype);
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



        rb = GetComponent<Rigidbody>();

        Controller = GetComponent<IController>();
        if (IsPlayer) Player = this;

        if (gameObject.tag != "Boss")
        {
            _weapon = GetComponentInChildren<Weapon>();
            _weapon.SetOwnerDUs(this);
        }
    }

    private void FixedUpdate()
    {
        Stats.Process();
    }

    private void UnitDying()
    {
        Vector3 rnd = new Vector3();
        Vector2 r;
        if (GoldPrefab != null)
        {
            for (int i = 0; i < GoldReward; i++)
            {
                r = Random.insideUnitCircle * 2;
                rnd.Set(r.x, 0, r.y);
                Coin a = Instantiate(GoldPrefab, transform.position + rnd, transform.rotation).GetComponent<Coin>();

                a.Target = Unit.Player;
                a.Initialize(Stats.Killer, Vector3.zero, Quaternion.identity); //use the source int as the killers id. This works only with coins.
            }
        }
        //if(UnitAnimation != null) UnitAnimation.SetBool("Death", true);
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
