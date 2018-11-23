using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public float startSpeed;
    float spe;
    public float speedStepPerFrame;
    public float MinDistance;
    public Unit target;


    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(12, 11);

        Physics.IgnoreLayerCollision(12, 12);
    }

    // Update is called once per frame
    void Update()
    {

        float distance;

        Vector3 vdir = (target.transform.position - transform.position);
        distance = vdir.magnitude;

        if (distance <= MinDistance)
        {
            target.stats.ApplyValue(Unit.StatType.GOLD, 1);
            Destroy(gameObject);
        }
        vdir.Normalize();
        transform.position += vdir * startSpeed * distance;
        startSpeed += speedStepPerFrame;
    }
}
