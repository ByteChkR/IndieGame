using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAoe : MonoBehaviour {

    public Vector3 bounderies;

	void Start () {
        Vector3 offset = new Vector3(0, 0, bounderies.z / 2);
        Collider[] test = Physics.OverlapBox(transform.position + offset, bounderies);
        for(int i = 0; i < test.Length;++i)
        {
            if(test[i].tag != "Player")
            {
                Unit enemyUnit = test[i].GetComponent<Unit>();

                if(enemyUnit ==null)
                {
                    return;
                }




            }

        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
