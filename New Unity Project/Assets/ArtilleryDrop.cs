using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryDrop : AbstractAbilityInstance
{

    public float DistanceFromCaster = 8;
    public Vector3 startHeight = new Vector3(0, 25, 0);
    // Use this for initialization
    void Start()
    {
        SetSpecialCollider(GetComponent<BoxCollider>());
        if(target == null)transform.position = source.transform.position + source.transform.forward * DistanceFromCaster + startHeight;
        else
        {
            transform.position = target.position + startHeight;
        }
    }

    RaycastHit info;
    bool hit;
    private void Update()
    {
        hit = Physics.Raycast(new Ray(transform.position, Vector3.down), out info, 1000, 1 << 10);
        if ((hit && info.distance < 0.5f) || !hit) Destroy(gameObject);
    }
}
