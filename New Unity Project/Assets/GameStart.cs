using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
        AchievementSystem.instance.ResetSystem();
        if (Unit.Player != null) Unit.Player.Stats.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
