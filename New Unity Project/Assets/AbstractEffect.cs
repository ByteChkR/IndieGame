using UnityEngine;
[System.Serializable]
public class AbstractEffect
{
    public Unit.StatType Type;
    public float Strength;
    public bool InverseOnTimeout = false;
    public float Duration;
    public float TimeActivated;
    public bool ApplyOnce = true;
    private bool _appliedOnce = false;
    private bool _inversed = false;

    public bool Active
    {
        get
        {
            return Time.realtimeSinceStartup < TimeActivated + Duration;
        }
    }

    public virtual float Apply(float value)
    {
        if (ApplyOnce && _appliedOnce) return value;
        _appliedOnce = true;
        return value + Strength;

    }

    public float Inverse(float value)
    {
        if (_inversed) return value;
        _inversed = true;
        return value - Strength;
    }
}