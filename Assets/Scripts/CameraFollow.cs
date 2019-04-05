using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform player;
    Vector3 dif;
    Vector3 StartLoc;
    public bool FreezCam = true;
    public enum FreezAgnle { x, y, z };
    public FreezAgnle frez;
    // Use this for initialization
    void Start() {
        dif = transform.position - player.position;
        StartLoc = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        //LevelForward = Vector3.ProjectOnPlane(cam.transform.forward, transform.up);
        //dir = LevelForward * Input.GetAxis("Vertical") + Vector3.Cross(LevelForward, transform.up) * -Input.GetAxis("Horizontal");
        //transform.rotation = Quaternion.LookRotation(dir);
        //tar.z = startPos.z;
        transform.position = player.position + dif;
        Vector3 loc = transform.localPosition;
        if (FreezCam) {
            if (frez == FreezAgnle.x) {
                loc.x = StartLoc.x;
            }
            if (frez == FreezAgnle.y) {
                loc.y = StartLoc.y;
            }
            if (frez == FreezAgnle.z) {
                loc.z = StartLoc.z;
            }
        }
        transform.localPosition = loc;
    }
}
