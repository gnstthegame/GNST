using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour {
    Camera cam;
    RaycastHit hit;
    Collider col, old;
    int mask;
    private void Awake() {
        cam = GetComponent<Camera>();
        mask = LayerMask.GetMask("UI");
    }
    // Update is called once per frame
    void Update () {

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask)) {
            col = hit.collider;
            if (old != col) {
                if(old!=null) old.SendMessage("MouseExit", SendMessageOptions.DontRequireReceiver);
                old = col;
                col.SendMessage("MouseEnter", SendMessageOptions.DontRequireReceiver);
            }
            if (Input.GetButtonDown("Fire1")) {
                col.SendMessage("MouseDown", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
