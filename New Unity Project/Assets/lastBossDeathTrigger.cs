using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastBossDeathTrigger : MonoBehaviour {

    private float _timeLeft = 0;
    private float _timeWanter = 4;
    public GameObject explosion;
	void Start () {
        AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffect.Explosion);
        Instantiate(explosion, transform.position, transform.rotation);
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
