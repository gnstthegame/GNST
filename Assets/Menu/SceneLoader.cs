using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string scene;
    SaveLinker SL;
    private void Awake() {
        SL = FindObjectOfType<SaveLinker>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SL.SavePlayer();
            SceneManager.LoadScene(scene);
        }
    }
}
