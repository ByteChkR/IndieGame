using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour {

	public AudioManager.BackgroundMusic backMusic;
	void Start () {
        if(AudioManager.instance != null)AudioManager.instance.ChangeBackgroundMusic(backMusic);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
