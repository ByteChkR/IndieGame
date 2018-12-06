using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{

    public GameObject PreFab;
    public int Rows = 10;
    public int Cols = 10;
    public float DistanceBetweenRows = 10;
    public float DistanceBetweenCols = 10;
    public float Speed = 1;
    public Vector3 Direction;
    Transform[] clouds;
    // Use this for initialization
    void Start()
    {
        Direction.Normalize();
        clouds = new Transform[Rows * Cols];
        Vector3 pos = transform.position;
        int i = 0;
        for (int R = 0; R < Rows; R++)
        {
            for (int C = 0; C < Cols; C++)
            {
                pos.Set(transform.position.x + R * DistanceBetweenRows, pos.y, transform.position.z + C * DistanceBetweenCols);
                clouds[i] = Instantiate(PreFab, pos, Quaternion.identity, transform).transform;
                i++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in clouds)
        {
            t.position += Direction * Speed;
            Vector3 t1 = t.position - transform.position;
            Debug.Log(t1.magnitude);
            if (t1.x >= Rows * DistanceBetweenRows) t1.x = 0;
            else if (t1.x <= 0) t1.x = Rows * DistanceBetweenRows;
            else if (t1.z >= Cols * DistanceBetweenCols) t1.z = 0;
            else if (t1.z <= 0) t1.z = Cols * DistanceBetweenCols;
            t.position = transform.position + t1;
        }
    }
}
