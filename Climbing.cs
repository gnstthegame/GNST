using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Climbing : MonoBehaviour {
    public float maxDist = 0.5f, maxAnge = 30, armLength = 1f;
    float betwen;
    bool drag = false;
    Transform box;
    Vector3 rail;
    void Start() {
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, maxDist);
            if (hit.transform.tag == "Climb" && Vector3.Angle(hit.normal, -transform.forward) < 30 && Vector3.Distance(transform.position, hit.point) < maxDist) {
                //rotate -hit.normal
                //anim.setTrigger("Climb");
            }
        }
        if (Input.GetButtonDown("Interact")) {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, maxDist);
            if (hit.transform.tag == "Move" && Vector3.Angle(hit.normal, -transform.forward) < 30 && Vector3.Distance(transform.position, hit.point) < maxDist) {
                Vector3 d = hit.transform.position - hit.point;
                d.Normalize();
                float df = Vector3.Dot(hit.transform.forward, d);
                float dr = Vector3.Dot(hit.transform.right, d);

                if (Mathf.Abs(df) > Mathf.Abs(dr)) {
                    if (df > 0) {//forw
                        rail = hit.transform.forward;
                    } else {//back
                        rail = -hit.transform.forward;
                    }
                } else {
                    if (dr > 0) {//right
                        rail = hit.transform.right;
                    } else {//left
                        rail = -hit.transform.right;
                    }
                }


                //rotate = -rail;
                //ram length
                //anim.setBool("Drag");
                betwen = Vector3.Distance(transform.position, hit.transform.position);
                drag = true;
            }
        }
        if (drag) {
            box.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * betwen);
        }
        if (Input.GetButtonUp("Interact")) {
            drag = false;
        }
        //hit.transform.position = transform.forward * betwen;
    }
}
