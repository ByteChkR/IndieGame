using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FootStepSound()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffect.FootSteps);
    }
}
