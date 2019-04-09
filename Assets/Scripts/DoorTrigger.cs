﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    public Transform pos;
    public ChangeCameraPosition cam;

    private void Awake() {
        if (cam == null) {
            cam = Camera.main.GetComponent<ChangeCameraPosition>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            cam.MoveCamera(pos);
        }
    }
}
