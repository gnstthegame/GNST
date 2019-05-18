using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyKey : MonoBehaviour {

    /// <summary>
    /// wyłącz po naciśnięciu klawisza
    /// </summary>
    private void Update() {
        if (Input.anyKeyDown) {
            gameObject.SetActive(false);
        }
    }
}
