using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastBossDeathTrigger : MonoBehaviour {

    private float _timeLeft = 0;
    private float _timeWanter = 4;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _timeLeft += Time.deltaTime;
        if(_timeLeft > _timeWanter)
        {
            GameEndScript.instance.ToWinScreen();
        }
	}
}
