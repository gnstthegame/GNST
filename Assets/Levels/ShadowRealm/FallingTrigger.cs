using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// klasa kontrolująca unoszenie płyt
/// </summary>
public class FallingTrigger : MonoBehaviour {
    public float BaseRadius = 15f;
    float radius;
    float speed;
    public float BaseSpeed = 0.1f;
    bool stop = false;

    /// <summary>
    /// podnieś płyty startowe
    /// </summary>
    void Awake() {
        radius = BaseRadius;
        speed = BaseSpeed;
        List<Collider> r = new List<Collider>(Physics.OverlapSphere(transform.position, radius));
        foreach (Collider c in r) {
            FallingBlock fa;
            if (fa = c.GetComponent<FallingBlock>()) {
                fa.upPose();
            }
        }
    }
    /// <summary>
    /// podnieś wszystkie płyty
    /// </summary>
    public void Win() {
        List<Collider> r = new List<Collider>(Physics.OverlapSphere(transform.position, 5f * BaseRadius));
        foreach (Collider c in r) {
            FallingBlock fa;
            if (fa = c.GetComponent<FallingBlock>()) {
                fa.fall = false;
            }
        }
        stop = true;
    }
    /// <summary>
    /// opuść wszystkie pływy
    /// </summary>
    void Lose() {
        List<Collider> f = new List<Collider>(Physics.OverlapSphere(transform.position, 5f * BaseRadius));
        foreach (Collider c in f) {
            FallingBlock fa;
            if (fa = c.GetComponent<FallingBlock>()) {
                fa.fall = true;
            }
        }
        radius = 0;
    }
    /// <summary>
    /// odnów zasięg budowania
    /// </summary>
    public void Contin() {
        radius = BaseRadius;
        speed = BaseSpeed;
    }

    /// <summary>
    /// unosi płyty w zasięgu, zwalnia płyty z niego wychodzące, zmniejsza zasięg budowania
    /// </summary>
    void Update() {
        if (!stop) {
            if (radius <= 1) {
                radius = 0;
                GetComponent<Collider>().enabled = false;
            } else {
                GetComponent<Collider>().enabled = true;
                speed = (BaseRadius - radius) / BaseRadius + 0.3f;
                radius -= Time.deltaTime * speed;
            }
            if (transform.position.y < -3) {
                //fall anim
                Lose();
            }
            List<Collider> r = new List<Collider>(Physics.OverlapSphere(transform.position, radius));
            List<Collider> f = new List<Collider>(Physics.OverlapSphere(transform.position, radius + 8f));
            f.RemoveAll(item => r.Contains(item) == true);

            foreach (Collider c in f) {
                FallingBlock fa;
                if (fa = c.GetComponent<FallingBlock>()) {
                    fa.fall = true;
                }
            }
            foreach (Collider c in r) {
                FallingBlock fa;
                if (fa = c.GetComponent<FallingBlock>()) {
                    fa.fall = false;
                }
            }
        }
    }
}
