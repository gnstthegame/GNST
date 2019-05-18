using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// menu pauzy
/// </summary>
public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    /// <summary>
    /// wznów gre
    /// </summary>
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    /// <summary>
    /// pauza
    /// </summary>
    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void TogglePause() {
        if (GameIsPaused) {
            Resume();
        } else {
            Pause();
        }
    }
    /// <summary>
    /// wyjdź
    /// </summary>
    public void QutGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
