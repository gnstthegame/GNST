using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Checkpoint : MonoBehaviour {
    SaveLinker SL;
    private void Awake() {
        SL=FindObjectOfType<SaveLinker>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SL.SavePlayer();
        }
    }

}
