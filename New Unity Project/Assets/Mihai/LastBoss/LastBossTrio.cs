using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossTrio : MonoBehaviour {

    public float speed;
    public float damage;
    public GameObject trioPrefab;
    private float _distance;
    private GameObject _lastBoss;
    public float creationOffset;
    private Rigidbody _rb;
    private GameObject _player;
    
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lastBoss = GameObject.FindGameObjectWithTag("Boss");
        _rb = GetComponent<Rigidbody>();

        transform.position += transform.forward * creationOffset;

        if (_lastBoss == null)
        {
            return;
        }
       
        _distance = _lastBoss.GetComponent<LastBoss>().GetDistance();
	}
	
	// Update is called once per frame
	void Update () {
        if (_lastBoss == null)
        {
            return;
        }

        _rb.velocity = transform.forward * speed;
        if(Vector3.Distance(transform.position,_lastBoss.transform.position)>_distance)
        {
            StartTrio();
        }
	}

    private void StartTrio()
    {
        if(_player!= null)
        {
            Vector3 sameheight = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            transform.LookAt(sameheight);
        }
        for(int i = 0; i <3; ++i)
        {
            GameObject trio = Instantiate(trioPrefab, transform.position, transform.rotation);
            trio.transform.Rotate(new Vector3(0, i * 120, 0));
            trio.GetComponent<FirstBossProjectile>().distanceoffset = 0;
        }
        //particles
        Destroy(gameObject);
    }

}
