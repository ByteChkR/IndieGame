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
        AudioManager.instance.GameOverScreen();
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
        if (AchievementSystem.instance.GetResultCoins()) AchievementOne.SetActive(false);            //Checking Achievements to display
        else AchievementOne.SetActive(true);
        if (AchievementSystem.instance.GetResultHealth()) AchievementTwo.SetActive(false);
        else AchievementTwo.SetActive(true);
        if (AchievementSystem.instance.GetResultKilling()) AchievementThree.SetActive(false);
        else AchievementThree.SetActive(true);
        if (AchievementSystem.instance.GetResultTime()) AchievementFour.SetActive(false);
        else AchievementFour.SetActive(true);

        AudioManager.instance.ChangeBackgroundMusic(AudioManager.BackgroundMusic.Result);
        MainMenu.SetActive(false);                                                                  //Setting the right screens active
        OptionsMenu.SetActive(false);
        WinScreen.SetActive(true);
        MenuCanvasBackground.SetActive(true);
        HUDScript.instance.ResetNpcStuff();

        AdditiveLevelManager.instance.RemoveLevel(1);                                               //Unload Levels
        AdditiveLevelManager.instance.RemoveLevel(2);
    }



	// Update is called once per frame
	void LateUpdate () {
	}
}
