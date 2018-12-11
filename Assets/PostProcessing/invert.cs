using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

public class invert : MonoBehaviour {
    public Light l;
    public bool asd = false, once=true;
    float inte;
    void Start() {
        inte = l.intensity;
    }
    IEnumerator Invers() {
        ArrayList mat = new ArrayList();
        Renderer[] rend = (FindObjectsOfType<Renderer>());
        foreach (Renderer r in rend) {
            foreach (Material i in r.materials) {
                if (i.shader.name == "Outline 1") {
                    mat.Add(i);
                }
            }
        }
        for (float i = 0; i < 1f; i += Time.deltaTime) {
            foreach (Material m in mat) {
                m.SetFloat("_slide", i);
            }
            float t = inte * (1 - i * 2.1f);
            if (t > 0f) {
                l.intensity = t;
            } else {
                l.intensity = 0;
            }
            yield return null;
        }
        l.intensity = 0;
    }
    IEnumerator Redo() {
        ArrayList mat = new ArrayList();
        Renderer[] rend = (FindObjectsOfType<Renderer>());
        foreach (Renderer r in rend) {
            foreach (Material i in r.materials) {
                if (i.shader.name == "Outline 1") {
                    mat.Add(i);
                }
            }
        }
        for (float i = 1; i > 0f; i -= Time.deltaTime) {
            foreach (Material m in mat) {
                m.SetFloat("_slide", i);
            }
            float t = inte * (1 - i*2.1f);
            if (t < 1f) {
                l.intensity = t;
            } else {
                l.intensity = 1;
            }
            yield return null;
        }
        l.intensity = 1;
    }
    void Update() {
        /*
        if (asd) {
            if (once) {
                StartCoroutine(Invers());
            } else {
                StartCoroutine(Redo());
            }
            once = !once;
            asd = false;
        }
        */
        if (asd) {
            if (once) {
                StartCoroutine(Invers());
                once = !once;
            }
        }

        }
}
