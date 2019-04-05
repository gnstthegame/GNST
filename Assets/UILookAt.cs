using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour {
    Canvas can;
    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        can = GetComponent<Canvas>();
        if (can.worldCamera == null) {
            can.worldCamera = cam;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up, 180f);

    }
}
