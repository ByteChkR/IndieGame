using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnStart : MonoBehaviour {

	public AudioManager.BackgroundMusic backMusic;
	void Start () {
        AudioManager.instance.ChangeBackgroundMusic(backMusic);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
