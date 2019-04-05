﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    public bool fall=true;
    bool infall=false;
    public float gravity = 1f;
    public float falDistance = 100f;
    public float rotLimit = 1f;
    public float smooth = 0.3f;
    public Transform tr;
    Vector3 startPos;
    Quaternion startRot;
    Vector3 down;

    Vector3 temp=Vector3.zero;

	// Use this for initialization
	void Awake () {
        transform.Rotate(transform.up, Random.Range(0,5)*90f);
        tr.position += Vector3.up * (Random.value - 0.5f) * 0.2f;
        startPos = tr.position;
        startRot = tr.rotation;
        down = new Vector3(0, -falDistance, 0);
        tr.position += down;
        tr.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
    }
    public void upPose() {
        tr.position= startPos;
        tr.rotation= startRot;
    }
	
	// Update is called once per frame
	void Update () {
        if (fall != infall) {
            infall = fall;
            StopAllCoroutines();
            if (fall) {
                StartCoroutine(Falling());
            } else {
                StartCoroutine(Rising());
            }
        }
    }
    IEnumerator Falling() {
        float grav = 0;
        Vector3 tarPos = startPos + down;
        Vector3 tarRot = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
        Quaternion startQ = tr.rotation;
        while ((tarPos- tr.position).magnitude > 1f) {
            grav += gravity;
            tr.position -= new Vector3(0, grav * Time.deltaTime, 0);
            //tr.Rotate();
            tr.rotation = Quaternion.RotateTowards(tr.rotation, Quaternion.LookRotation(tarRot), rotLimit);
            yield return 0;
        }

    }
    IEnumerator Rising() {
        float dist = Vector3.Distance(startPos, tr.position);
        float diff = Vector3.Distance(startPos, tr.position);
        Quaternion fromRot = tr.rotation;
        while (diff > 0.01f) {
            tr.position = Vector3.SmoothDamp(tr.position, startPos, ref temp, smooth);
            diff = Vector3.Distance(startPos, tr.position);
            tr.rotation = Quaternion.Slerp(fromRot, startRot, 1f - diff /dist);
            yield return 0;
        }
    }

}
