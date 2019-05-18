using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// trigger wczytujący poziom
/// </summary>
public class SceneLoader : LevelLoader {
    public string scene;
    bool inside=false;
    SaveLinker SL;
    private void Awake() {
        SL = FindObjectOfType<SaveLinker>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            inside = true;
            SL.SavePlayer();
            LoadLevel(scene);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            inside = false;
        }
    }
    private void Update() {
        if(inside && Input.GetButtonDown("Interact")) {
            SL.SavePlayer();
            LoadLevel(scene);
        }
    }
}
