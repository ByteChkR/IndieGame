using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSound : MonoBehaviour
{

    public float MaxDistance;
    public float MinDistance;


    public static List<SpeakerSound> spkrSnd = new List<SpeakerSound>();

    private void Awake()
    {
        spkrSnd.Add(this);
    }

    public float CalculateVolume(Vector3 v)
    {
        float distance = (transform.position - v).magnitude;
        if (distance > MaxDistance) return 0;
        if (distance <= MinDistance) return 1;
        else return 1 - ((distance - MinDistance) / (MaxDistance - MinDistance));
    }

    public static bool UseSpeaker3D = false;

    public static float GetVolume(Vector3 v)
    {
        if (Unit.Player == null||!UseSpeaker3D) return 1;
        int mul = spkrSnd.Count;
        float d = 0;
        float t = 0;
        if (mul == 0) return 1;
        foreach (SpeakerSound speaker in spkrSnd)
        {
            t = speaker.CalculateVolume(Unit.Player.transform.position);
            if (t > d)
                d = t;
        }
        return d;
    }

    private void OnDestroy()
    {
        spkrSnd.Remove(this);
    }
}
