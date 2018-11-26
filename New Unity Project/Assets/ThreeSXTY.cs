﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ThreeSXTY : Ability
{
    [SerializeField]
    private readonly string _animationName;
    [SerializeField]
    private readonly float _animationSpeed;
    [SerializeField]
    private List<AbstractEffect> _onHitEffects;
    private Dictionary<int, Vector3> _localCoords = new Dictionary<int, Vector3>();//The coords on the sword



    private bool _started = false;
    // Use this for initialization
    void Start()
    {

    }

    public override void Initialize(int source, Vector3 target, Quaternion rot)
    {
        Collider = Unit.ActiveUnits[source].SelectedWeapon.Coll;
        base.Initialize(source, target,rot);

        if (Source.UnitAnimation[_animationName] != null)
        {
            Source.UnitAnimation[_animationName].speed = _animationSpeed;
            Source.UnitAnimation.Play(_animationName, PlayMode.StopSameLayer);
        }
        _started = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (!Initialized)
        {
            return;
        }
        if (_started && !Source.UnitAnimation.isPlaying)
        {
            _started = false;
            Destroy(this.gameObject);
        }
        else
        {
            foreach (int i in UnitsHitSinceInit)
            {
                Unit.ActiveUnits[i].transform.position = Collider.transform.TransformPoint(_localCoords[i]);
            }
        }
    }

    public override void OnDestroy()
    {
        UnitsHitSinceInit.Where(x => Unit.ActiveUnits.ContainsKey(x)).ToList().ForEach(x => Unit.ActiveUnits[x].Stats.ApplyValue(Unit.StatType.STUN, -1));

        base.OnDestroy();

    }

    public override void OnHit(Unit target)
    {
        _localCoords.Add(target.gameObject.GetInstanceID(), Collider.transform.InverseTransformPoint(target.transform.position));
        target.Stats.AddEffects(_onHitEffects.ToArray(), Source.gameObject.GetInstanceID());
        target.Stats.ApplyValue(Unit.StatType.STUN, 1);
        base.OnHit(target);
    }


}
