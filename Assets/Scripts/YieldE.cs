using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// zmienia położenie ui względem objektów na mapie
/// </summary>
public class YieldE : MonoBehaviour {
    public Transform track, panel;
    Text txt;
    private void Awake() {
        txt = GetComponentInChildren<Text>();
    }
    void Update() {
        panel.transform.position = Camera.main.WorldToScreenPoint(track.transform.position);
    }
    public void Show(string tekst) {
        txt.enabled = true;
        txt.text = tekst;
    }
    public void Hide() {
        txt.enabled = false;
    }
}
