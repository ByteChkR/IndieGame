using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriveOnStart : MonoBehaviour
{

    public GameObject Train;
    public float TrainStartTime = 5;
    public float TrainDriveDistance = 50;
    public float TimeToFullspeed = 10;
    float t = 0;
    float pos = 0;
    Vector3 startPos;
    bool incoming = false;
    // Use this for initialization
    void Start()
    {
        startPos = Train.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= TrainStartTime)
        {
            float t1 = (t - TrainStartTime);
            float t2 = t1 / TimeToFullspeed;


            t2 = Mathf.SmoothStep(0, 1, t2);
            
            if (!incoming) Train.transform.position = Vector3.Lerp(startPos, startPos + Vector3.left * TrainDriveDistance, t2);
            else Train.transform.position = Vector3.Lerp(startPos - Vector3.left * TrainDriveDistance, startPos, t2);

            if (t2 >= 1)
            {
                t = 0;
                incoming = !incoming;
            }
        }
    }
}
