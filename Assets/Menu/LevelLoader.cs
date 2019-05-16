using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject LoadingScreen;
    public Slider slider;
    AsyncOperation operation;

    public void LoadLevel(int sceneIndex) {
        LoadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronosly());
    }
    public void LoadLevel(string sceneName) {
        LoadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadAsynchronosly());
    }

    IEnumerator LoadAsynchronosly() {
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }

}
