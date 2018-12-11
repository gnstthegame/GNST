using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomSlide : MonoBehaviour {
    public Vector3 maxRecoil;
    public float speed;
    float inter = 1;
    public Vector3 StartPos, from, to;
    void Start() {
        StartPos = transform.position;
    }

    void Update() {

        if (inter >= 1) {
            to = new Vector3(Random.Range(-maxRecoil.x, maxRecoil.x), Random.Range(-maxRecoil.y, maxRecoil.y), Random.Range(-maxRecoil.z, maxRecoil.z));
            to += StartPos;
            inter = 0;
            from = transform.position;
        } else {
            inter += speed*Time.deltaTime;
        }
        transform.position = Vector3.Lerp(from, to, inter);
    }
}
