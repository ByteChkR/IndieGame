using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingFirstBoss : MonoBehaviour {

    // Use this for initialization
    public GameObject explosion;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void DestroyBoss()
    {
        Destroy(gameObject);
        AudioManager.instance.ChangeBackgroundMusic(AudioManager.BackgroundMusic.Stage1);
        AudioManager.instance.PlaySoundEffect(AudioManager.SoundEffect.Explosion);
        Instantiate(explosion, transform.position, transform.rotation);
        GameObject.Find("BossDoorOpenMihai").transform.GetChild(0).GetComponent<OpenBossDoor>().OpenDoor();
    }
}
