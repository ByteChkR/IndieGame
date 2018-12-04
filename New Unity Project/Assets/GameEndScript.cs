using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScript : MonoBehaviour {

    public static GameEndScript instance;
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject MenuCanvasBackground;
    public GameObject AchievementOne;
    public GameObject AchievementTwo;
    public GameObject AchievementThree;
    public GameObject AchievementFour;

    // Use this for initialization
    void Awake () {
        instance = this;                        //Creating Singleton
    }
	
    public void ToGameOver()
    {
        MainMenu.SetActive(false);                                                                  //Setting right screens active
        OptionsMenu.SetActive(false);
        GameOverScreen.SetActive(true);
        MenuCanvasBackground.SetActive(true);
        HUDScript.instance.ResetNpcStuff();
        AdditiveLevelManager.instance.RemoveLevel(1);                                               //Unload Levels
        AdditiveLevelManager.instance.RemoveLevel(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ToWinScreen()
    {
        if (AchievementSystem.instance.GetResultCoins()) AchievementOne.SetActive(true);            //Checking Achievements to display
        else AchievementOne.SetActive(false);
        if (AchievementSystem.instance.GetResultHealth()) AchievementTwo.SetActive(true);
        else AchievementTwo.SetActive(false);
        if (AchievementSystem.instance.GetResultKilling()) AchievementThree.SetActive(true);
        else AchievementThree.SetActive(false);
        if (AchievementSystem.instance.GetResultTime()) AchievementFour.SetActive(true);
        else AchievementFour.SetActive(false);

        MainMenu.SetActive(false);                                                                  //Setting the right screens active
        OptionsMenu.SetActive(false);
        WinScreen.SetActive(true);
        MenuCanvasBackground.SetActive(true);

        AdditiveLevelManager.instance.RemoveLevel(1);                                               //Unload Levels
        AdditiveLevelManager.instance.RemoveLevel(2);
    }



	// Update is called once per frame
	void LateUpdate () {
	}
}
