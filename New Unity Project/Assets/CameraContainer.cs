using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
public class CameraContainer : MonoBehaviour
{
    public string Key;
    public Transform PointPrefab;

    private void Start()
    {
        SmoothLine();
    }
    public Transform[] LoadTransforms()
    {
        Transform[] childs = new Transform[transform.childCount];
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i);
        }
        return childs;
    }

    public List<KeyValuePair<Vector3, Quaternion>> ConvertTransforms(Transform[] transforms)
    {
        List<KeyValuePair<Vector3, Quaternion>> ret = new List<KeyValuePair<Vector3, Quaternion>>();
        foreach (Transform transform in transforms)
        {
            ret.Add(new KeyValuePair<Vector3, Quaternion>(transform.position, transform.rotation));
        }
        return ret;
    }

    public void DestroyTransforms(Transform[] transforms)
    {
        foreach (Transform t in transforms)
        {
            DestroyImmediate(t.gameObject);
        }
    }

    public Transform[] CreateTransforms(List<KeyValuePair<Vector3, Quaternion>> pts)
    {
        Transform[] transforms = new Transform[pts.Count];
        for (int i = 0; i < pts.Count; i++)
        {
            transforms[i] = Instantiate(PointPrefab, pts[i].Key, pts[i].Value, transform);
        }
        return transforms;
    }

    public void SmoothLine()
    {
        Transform[] childs = LoadTransforms();
        List<KeyValuePair<Vector3, Quaternion>> pts = Chaikin(ConvertTransforms(childs));
        DestroyTransforms(childs);
        childs = CreateTransforms(pts);
    }



    private static List<KeyValuePair<Vector3, Quaternion>> Chaikin(List<KeyValuePair<Vector3, Quaternion>> pts)
    {
        KeyValuePair<Vector3, Quaternion>[] newPts = new KeyValuePair<Vector3, Quaternion>[(pts.Count - 2) * 2 + 2];
        newPts[0] = pts[0];
        newPts[newPts.Length - 1] = pts[pts.Count - 1];

        int j = 1;
        for (int i = 0; i < pts.Count - 2; i++)
        {
            newPts[j] = new KeyValuePair<Vector3, Quaternion>(pts[i].Key + (pts[i + 1].Key - pts[i].Key) * 0.75f,
                Quaternion.Lerp(pts[i].Value, pts[i + 1].Value, 0.75f));
            newPts[j + 1] = new KeyValuePair<Vector3, Quaternion>(pts[i + 1].Key + (pts[i + 2].Key - pts[i + 1].Key) * 0.25f,
                Quaternion.Lerp(pts[i + 1].Value, pts[i + 2].Value, 0.25f));
            j += 2;
        }
        return newPts.ToList();
    }
}