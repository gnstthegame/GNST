using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject LoadingScreen;
    public Slider slider;

    public void LoadLevel (int sceneIndex)
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronosly(sceneIndex));

    }

    IEnumerator LoadAsynchronosly (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            
            yield return null;
        }
    }

}
