using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E : MonoBehaviour {
    public Transform track;
    void Update() {
        transform.position = Camera.main.WorldToScreenPoint(track.transform.position);
    }
}
