using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour {
    public float radius = 5f;

    // Use this for initialization
    void Start() {
        List<Collider> r = new List<Collider>(Physics.OverlapSphere(transform.position, radius));
        foreach (Collider c in r) {
            FallingBlock fa;
            if (fa = c.GetComponent<FallingBlock>()) {
                fa.upPose();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        List<Collider> r = new List<Collider>(Physics.OverlapSphere(transform.position, radius));
        List<Collider> f = new List<Collider>(Physics.OverlapSphere(transform.position, radius+8f));
        f.RemoveAll(item => r.Contains(item)==true);

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
