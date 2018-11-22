﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSXTY : Ability
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _animationName;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> onHitEffects;
    Dictionary<int, Vector3> localCoords = new Dictionary<int, Vector3>();//The coords on the sword



    bool started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target)
    {
        _collider = Unit.ActiveUnits[source].SelectedWeapon.coll;
        base.Initialize(source, target);

        Source.UnitAnimation[_animationName].speed = _animationSpeed;
        Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        started = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (started && !Source.UnitAnimation.isPlaying)
        {
            started = false;
            Destroy(this.gameObject);
        }
        else
        {
            foreach (int i in unitsHitSinceInit)
            {
                Unit.ActiveUnits[i].transform.position = _collider.transform.TransformPoint(localCoords[i]);
            }
        }
    }

    public override void OnDestroy()
    {
        unitsHitSinceInit.ForEach(x => Unit.ActiveUnits[x].stats.ApplyValue(Unit.StatType.STUN, -1));
        
        base.OnDestroy();

    }

    public override void OnHit(Unit target)
    {
        localCoords.Add(target.gameObject.GetInstanceID(), _collider.transform.InverseTransformPoint(target.transform.position));
        target.stats.ApplyValue(Unit.StatType.HP, -_damage);
        target.stats.AddEffects(onHitEffects.ToArray());
        target.stats.ApplyValue(Unit.StatType.STUN, 1);
        base.OnHit(target);
    }


}
