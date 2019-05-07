using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    public bool fall = true;
    bool infall = true;
    public float gravity = 1f;
    public float falDistance = 100f;
    public float rotLimit = 1f;
    public float smooth = 0.3f;
    public bool DontRotate = false;
    public Transform tr;
    public SpriteRenderer Rend;
    public Sprite[] symbols = new Sprite[10];
    Vector3 startPos;
    Quaternion startRot;
    Vector3 down;
    Vector3 temp = Vector3.zero;

    void Awake() {
        if (!DontRotate) {
            transform.Rotate(transform.up, Random.Range(0, 5) * 90f);
        }
        tr.position += Vector3.up * (Random.value - 0.5f) * 0.2f;
        startPos = tr.position;
        startRot = tr.rotation;
        down = new Vector3(0, -falDistance, 0);
        tr.position += down;
        tr.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
        if (Rend != null) {
            Rend.enabled = false;
        }
    }
    public void upPose() {
        tr.position = startPos;
        tr.rotation = startRot;
    }
    public void Mark(int s) {
        Rend.enabled = true;
        if (s < 4) {
            Rend.sprite = symbols[0];
            Rend.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, s * 90 - startRot.eulerAngles.y));
        } else {
            Rend.sprite = symbols[s - 3];
            Rend.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -startRot.eulerAngles.y));
        }
    }
    
    void Update() {
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
        while ((tarPos - tr.position).magnitude > 1f) {
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
            tr.rotation = Quaternion.Slerp(fromRot, startRot, 1f - diff / dist);
            yield return 0;
        }
    }

}
