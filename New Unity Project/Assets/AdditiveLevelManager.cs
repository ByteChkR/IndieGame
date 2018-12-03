using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

using System.Linq;
public class AdditiveLevelManager : MonoBehaviour
{
    public GameObject Player;
    public static AdditiveLevelManager instance;
    private Dictionary<int, MapInfo> loadedLevels = new Dictionary<int, MapInfo>();
    public string LevelPrefix = "level_";
    public bool DebugStart = false;
    private Unit player;
    public GameObject IngameHud;
    public Slider loadingSlider;
    public GameObject loadingScreen;
    public Text progressText;
    public GameObject menuScreen;
    public GameObject optionsMenu;
    public GameObject MenuCavasBackground;
    public int HighestLevel = 0;
    public float LastGold = 0;
    public int HighestCheckpoint = 0;
    public int _lastWeaponID;

    public int HighestLoadedLevel()
    {
        return loadedLevels.Max(x => x.Key);
    }

    // Use this for initialization
    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if(playerGameObject!=null)
        {

        player =playerGameObject.GetComponent<Unit>();
        }
        UnityEngine.Debug.Assert(instance == null, "Instance singleton is already initialized.");
        instance = this;
        loadedLevels = new Dictionary<int, MapInfo>();
        if (DebugStart) LoadLevel(1);
    }

    private void Update()
    {
    }

    public void LoadLevel(int index)
    {

        if (loadedLevels.ContainsKey(index)) return;
        if (index == 0)
        {
            IngameHud.SetActive(false);
        }
        else
        {
            IngameHud.SetActive(true);
        }
        StartCoroutine(LoadAsynchronously(index));

    }

    public void RemoveLevel(int index)
    {
        if (!loadedLevels.ContainsKey(index)) return;

        SceneManager.UnloadSceneAsync(index).completed += OnUnloadComplete;
        loadedLevels.Remove(index);
    }

    void OnUnloadComplete(AsyncOperation op)
    {
    }

    public void ClearLevels()
    {
        //DataManager.instance.PlayerStats.FixStats(true);
        foreach (KeyValuePair<int, MapInfo> item in loadedLevels)
        {

            SceneManager.UnloadSceneAsync(item.Key).completed += OnUnloadComplete;
        }
        loadedLevels.Clear();
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        loadingScreen.transform.parent.gameObject.SetActive(true);
        loadingScreen.SetActive(true);
        menuScreen.SetActive(false);
        if (Unit.Player != null) Unit.Player.ToggleUnitMovement(false);
        //c.EnableMovement = false;
        //c.EnableRotation = false;
        IngameHud.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (loadingSlider != null) loadingSlider.value = progress;
            if (progressText != null) progressText.text = Mathf.Round(progress * 100f) + "%";


            yield return null;
        }
        menuScreen.SetActive(true);
        loadingScreen.SetActive(false);
        optionsMenu.SetActive(false);
        if (sceneIndex > 0)
        {
            IngameHud.SetActive(true);
            loadingScreen.transform.gameObject.SetActive(false);
            menuScreen.SetActive(false);
            MenuCavasBackground.SetActive(false);
            
        }
        if(HighestLevel < sceneIndex)
        {
            HighestLevel = sceneIndex;
            HighestCheckpoint = 0;

        }

        loadedLevels.Add(sceneIndex, null);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
        //MapInfo level = GameObject.Find(LevelPrefix + sceneIndex).GetComponent<MapInfo>();
        //UnityEngine.Debug.Assert(level != null, "Level prefix is not correct, you tried to load: " + LevelPrefix + sceneIndex + ", Check the GameObject name of the level.");
        //loadedLevels.Add(sceneIndex, level);


    }

    
    public void Reset()
    {
        //IResettable[] resettableObjs = (IResettable[])GameObject.FindObjectsOfType<IResettable>());            
        //foreach (IResettable ir in resettableObjs)
        //{
        //    ir.Reset();
        //}
        RemoveLevel(HighestLevel);
        GameEndScript.instance.GameOverScreen.SetActive(false);
        LoadLevel(HighestLevel);
    }
    
}
