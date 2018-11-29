using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : Ability
{

    public float DelayPerProjectile;
    public int ProjectileCount;
    public GameObject Projectile;
    public float DistanceBetweenProjectiles;
    public float StartDistance;
    private float _actualDistance
    {
        get
        {
            return StartDistance + DistanceBetweenProjectiles * _currentCount;
        }
    }
    private int _currentCount = 0;
    private float _timeInitialized;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);
        _timeInitialized = Time.realtimeSinceStartup;

    }

    public override void Update()
    {
        base.Update();
    }

    public void FixedUpdate()
    {
        if (_currentCount < (int)((Time.realtimeSinceStartup - _timeInitialized) / DelayPerProjectile))
        {
            if (Source != null)
            {
                Ability s = Instantiate(Projectile, transform.position, transform.rotation).GetComponent<Ability>();
                s.SetAnimState(state);
                s.Initialize(Source.gameObject.GetInstanceID(), transform.position + transform.forward * _actualDistance, TargetRot, isSpecial);
            }
            _currentCount++;
        }
        if (_currentCount >= ProjectileCount) Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnHit(Unit target)
    {
        base.OnHit(target);
    }

}
