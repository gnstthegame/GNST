using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour {

    void Start() {
        //StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0, 0, -20);

    }
    IEnumerator Rotate() {
        while (true) {
            transform.Rotate(0, 0, -20);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
