using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class AdditiveLevelManager : MonoBehaviour
{

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


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
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
        //DataManager.instance.PlayerStats.FixStats(false);
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
        Controller c = player.GetComponent<Controller>();
        if (player != null && player.rb != null) player.rb.constraints = RigidbodyConstraints.FreezeRotation + (int)RigidbodyConstraints.FreezePositionY;
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
            loadingScreen.transform.parent.gameObject.SetActive(false);
        }
        MapInfo level = GameObject.Find(LevelPrefix + sceneIndex).GetComponent<MapInfo>();
        UnityEngine.Debug.Assert(level != null, "Level prefix is not correct, you tried to load: " + LevelPrefix + sceneIndex + ", Check the GameObject name of the level.");
        loadedLevels.Add(sceneIndex, level);

        if (!level.data.isMenu) 
        player.rb.constraints = RigidbodyConstraints.FreezeRotation + (int)RigidbodyConstraints.FreezePositionY;
        else if (!level.data.isTurorial)
        {
            // c.EnableMovement = false;
            // c.EnableRotation = false;
        }
        else
        {
            //c.EnableMovement = true;
            //c.EnableRotation = true;
        }
    }

    /*
    public void Reset()
    {
        IResettable[] resettableObjs = (IResettable[])GameObject.FindObjectsOfType(typeof(IResettable));
        foreach (IResettable ir in resettableObjs)
        {
            ir.Reset();
        }
    }
    */
}
