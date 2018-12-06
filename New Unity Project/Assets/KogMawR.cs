using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogMawR : Ability
{

    public GameObject exposion;
    public AbstractEffect[] Effects;
    public Vector3 Offset;

    private RaycastHit _info;
    private bool _hit;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        _hit = Physics.Raycast(new Ray(transform.position, Vector3.down), out _info, 1000, 1 << 10);
        if ((_hit && _info.distance < 0.5f) || !_hit) Destroy(gameObject);
    }

    public override void Initialize(int source, Vector3 target, Quaternion rot, bool isSpecial)
    {
        base.Initialize(source, target, rot, isSpecial);
        transform.position = target + Offset;
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
    }
    public override void OnHit(Unit target)
    {
        if(target == Unit.Player)
        {
            Instantiate(exposion, transform.position, transform.rotation);
            AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffect.Explosion);
            
        }
        base.OnHit(target);
        target.Stats.AddEffects(Effects, Source.gameObject.GetInstanceID());
    }
}
