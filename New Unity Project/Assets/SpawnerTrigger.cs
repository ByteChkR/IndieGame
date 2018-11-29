using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour {

    public List<GameObject> spawners;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for(int i =0; i < spawners.Count;++i)
            {
                spawners[i].GetComponent<Spawner>().StartSpawn();
            }
            Destroy(gameObject);
        }
    }
}
