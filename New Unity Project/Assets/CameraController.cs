using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    CameraContainer CameraTransformContainer;
    public GameObject[] CameraTransformContainerPrefabs;
    List<CameraContainer> _CameraConainers;
    public float TravelTime = 10;

    List<KeyValuePair<Vector3, Quaternion>> l = new List<KeyValuePair<Vector3, Quaternion>>();
    List<KeyValuePair<float, Vector3>> deltas;
    public bool start = false;
    float totalDistance;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        _CameraConainers = new List<CameraContainer>();
        foreach (GameObject gameObject in CameraTransformContainerPrefabs)
        {
            _CameraConainers.Add(Instantiate(gameObject).GetComponent<CameraContainer>());
        }

    }

    float curT = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!start) return;
        curT += Time.fixedDeltaTime;
        if (curT >= TravelTime)
        {
            start = false;
            curT = 0;
            
            return;
        }
        float normTime;
        int next;
        
        GetTimeAndNode(curT, out next, out normTime);
        
        ApplyMovement(next, normTime);

    }

    void ApplyMovement(int next, float time)
    {
        transform.rotation = Quaternion.Lerp(l[next - 1].Value, l[next].Value, time);
        transform.position = Vector3.Lerp(l[next - 1].Key, l[next].Key, time);
    }

    public void Load(string key)
    {
        foreach (CameraContainer cc in _CameraConainers)
        {
            if (cc.Key == key)
            {
                l = cc.ConvertTransforms(cc.LoadTransforms());
                totalDistance = GetTotalTravelLength(out deltas);
                start = true;
                return;
            }
        }
    }
    public Vector3 GetLast()
    {
        return l.Last().Key;
    }

    void GetTimeAndNode(float totalCurrentTime, out int next, out float time)
    {
        float targetDistance = totalDistance * (totalCurrentTime / TravelTime);
        float cur = 0;
        next = deltas.Count-1;
        time = 1;
        for (int i = 0; i < deltas.Count; i++)
        {
            if (cur >= targetDistance)
            {
                next = i;
                time = (deltas[i - 1].Key - (cur - targetDistance)) / deltas[i - 1].Key;
                return;
            }

            cur += deltas[i].Key;
        }
    }

    float GetTotalTravelLength(out List<KeyValuePair<float, Vector3>> DistanceAndNormalVector)
    {
        float distance = 0;

        Vector3 dir;
        float delta;
        DistanceAndNormalVector = new List<KeyValuePair<float, Vector3>>(l.Count);
        for (int i = 1; i < l.Count; i++)
        {
            dir = (l[i].Key - l[i - 1].Key);
            delta = dir.magnitude;
            distance += delta;
            DistanceAndNormalVector.Add(new KeyValuePair<float, Vector3>(delta, dir));
        }

        return distance;

    }

}
