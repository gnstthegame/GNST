using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomSlide : MonoBehaviour {
    public Vector3 maxRecoil;
    public float speed;
    float inter = 1;
    Vector3 StartPos, from, to;
    void Start() {
        StartPos = transform.localPosition;
        from = StartPos;
    }

    void Update() {
        transform.position = Vector3.Lerp(from, to, inter);

        if (inter >= 1) {
            to = new Vector3(Random.Range(StartPos.x - maxRecoil.x, StartPos.x + maxRecoil.x), Random.Range(StartPos.x - maxRecoil.y, StartPos.x + maxRecoil.y), Random.Range(StartPos.x - maxRecoil.z, StartPos.x + maxRecoil.z));
            inter = 0;
            from = transform.localPosition;
        } else {
            inter = speed*Time.deltaTime;
        }
    }
}
