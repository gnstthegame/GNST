using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string scene;
    SaveLinker SL;
    private void OnValidate() {
        SL = FindObjectOfType<SaveLinker>();
    }
    private void Start() {
        if (SL == null) {
            SL = FindObjectOfType<SaveLinker>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SL.SavePlayer();
            SceneManager.LoadScene(scene);
        }
    }
}
