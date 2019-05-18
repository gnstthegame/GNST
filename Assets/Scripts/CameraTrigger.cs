using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {
    public Transform pos;
    public Vector3 distance;
    public bool x=true, y=true, z=true;
    public bool ThirdPerson;
    CameraFollow cam;

    private void Awake() {
        if (cam == null) {
            cam = Camera.main.GetComponent<CameraFollow>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (ThirdPerson) {
                cam.MoveCamera(distance);
            } else {
                cam.MoveCamera(pos,distance,x,y,z);
            }
        }
    }
}
