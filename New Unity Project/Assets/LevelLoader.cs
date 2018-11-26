using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    #region "Inspector"
    public GameObject LoadingScreen;
    public Slider Slider;
    public Text ProgressText;
    #endregion

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        Cursor.visible = false;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            Slider.value = progress;
            ProgressText.text = Mathf.Round(progress * 100f) + "%";

            yield return null;
        }

    }

}
