using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// umożliwia wybieranie pól interaktywnych
/// </summary>
public class CameraRaycast : MonoBehaviour {
    Camera cam;
    RaycastHit hit;
    Collider col, old;
    int mask;
    /// <summary>
    /// przypisuje startowe komponenty
    /// </summary>
    private void Awake() {
        cam = GetComponent<Camera>();
        mask = LayerMask.GetMask("UI");
    }
    /// <summary>
    /// śledzi ruchy myszy względem pól walki
    /// </summary>
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
