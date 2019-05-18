using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// zapisuje stan gry przy wejściu w trigger
/// </summary>
public class Checkpoint : MonoBehaviour {
    SaveLinker SL;
    private void Awake() {
        SL=FindObjectOfType<SaveLinker>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SL.SavePlayer();
        }
        //Destroy(this);
    }

}
