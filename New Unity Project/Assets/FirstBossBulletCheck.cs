using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreateBeforeDestroy))]
public class FirstBossBulletCheck : MonoBehaviour {

    public float damage;
	void Start () {
        Collider[] testColliders = Physics.OverlapSphere(transform.position, 0.5f);
        for(int i = 0; i < testColliders.Length;++i)
        {
            if(testColliders[i].gameObject.tag == "Player")
            {
                ResolvePlayerCollision(testColliders[i].gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ResolvePlayerCollision(other.gameObject);
        }
    }

    private void ResolvePlayerCollision(GameObject pl)
    {
        Unit unitTest = pl.GetComponent<Unit>();
        if(unitTest != null)
        {
            unitTest.Stats.ApplyValue(Unit.StatType.HP,-damage, 90);
            CreateBeforeDestroy cbd = GetComponent<CreateBeforeDestroy>();
            if(cbd!= null)
            {
                cbd.Create();
            }
            Destroy(gameObject);
        }
    }
}
