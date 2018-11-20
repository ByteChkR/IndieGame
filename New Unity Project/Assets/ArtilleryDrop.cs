using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryDrop : AbstractAbilityInstance {

    public float DistanceFromCaster = 8;
    public Vector3 startHeight = new Vector3(0, 25, 0);
    // Use this for initialization
	void Start () {
        SetSpecialCollider(GetComponent<BoxCollider>());
        transform.position = source.transform.position + source.transform.forward * DistanceFromCaster + startHeight;
        Debug.Log((source.vDirNorm * DistanceFromCaster).magnitude);
	}
	

    private void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), 0.5f, 1 << 10)) Destroy(gameObject);
    }
}
