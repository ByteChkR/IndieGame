using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossSpecialBullet : MonoBehaviour {

    private Transform _player;
    public  float maxTimeTochangeDirection;
    private float _currentTime = 0;
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime;

        if(_currentTime >maxTimeTochangeDirection)
        {
            _currentTime = 0;
            Vector3 sameheight = new Vector3(_player.position.x, transform.position.y, _player.transform.position.z);
            transform.LookAt(sameheight);
        }
	}
}
