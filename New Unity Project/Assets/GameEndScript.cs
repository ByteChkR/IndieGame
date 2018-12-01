using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScript : MonoBehaviour {

    public static GameEndScript instance;
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject MainMenu;
    public GameObject OptionsMenu;

	// Use this for initialization
	void Start () {
        instance = this;
    }
	
    public void ToGameOver()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        GameOverScreen.SetActive(true);
    }

    public void ToWinScreen()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        WinScreen.SetActive(true);
        AdditiveLevelManager.instance.RemoveLevel(1);
        AdditiveLevelManager.instance.RemoveLevel(2);
    }


	// Update is called once per frame
	void LateUpdate () {
        if (Unit.Player == null) ToGameOver();
	}
}
