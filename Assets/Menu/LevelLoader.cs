using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// obsługa wczytywania gry
/// </summary>
public class LevelLoader : MonoBehaviour {

    public GameObject LoadingScreen;
    public Slider slider;
    AsyncOperation operation;

    /// <summary>
    /// wczytaj poziom
    /// </summary>
    /// <param name="sceneIndex">index</param>
    public void LoadLevel(int sceneIndex) {
        LoadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadAsynchronosly());
    }
    /// <summary>
    /// wczytaj poziom
    /// </summary>
    /// <param name="sceneIndex">nazwa</param>
    public void LoadLevel(string sceneName) {
        LoadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadAsynchronosly());
    }
    /// <summary>
    /// restartuj poziom
    /// </summary>
    public void RestartLevel() {
        LoadingScreen.SetActive(true);
        operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        StartCoroutine(LoadAsynchronosly());
    }
    /// <summary>
    /// rutyna wczytująca poziom w tle
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAsynchronosly() {
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }

}
