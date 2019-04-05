using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {
    public float maxDist = 0.5f, armLength = 1f, prepereSpeed = 2, climbDistans = 14;
    public float moc;
    Animator anim;
    Vector3 LevelForward, dir = Vector3.forward;
    public bool frez = false, draging = false, prepere = false;
    Collider cl;
    CapsuleCollider colider;
    public Camera cam;
    Coroutine routine;
    GameObject go;
    [SerializeField]
    bool grounded = false;
    static Vector3 UpOfset = new Vector3(0, 0.05f, 0);

    void Start() {
        if (cam == null) {
            cam = Camera.main;
        }
        colider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        LevelDir();
    }

    void Update() {
        if (!draging) {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                if (Input.GetButton("Sprint")) {
                    anim.SetFloat("Forward", 2f, 0.2f, Time.deltaTime);
                } else {
                    anim.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
                }
                dir = LevelForward * Input.GetAxis("Vertical") + Vector3.Cross(LevelForward, transform.up) * -Input.GetAxis("Horizontal");
                if (!frez) {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 10f);
                }
            } else {
                anim.SetFloat("Forward", 0, 0.2f, Time.deltaTime);
                anim.SetFloat("InputVertical", 0, 0.2f, Time.deltaTime);
            }
        }

        if (Input.GetButtonDown("Dash")) {
            transform.rotation = Quaternion.LookRotation(dir);
            anim.SetTrigger("Dash");
            frez = true;
        }


        if (Input.GetButtonDown("Jump") && routine == null) {
            routine = StartCoroutine(Climb());
        }
        if (Input.GetButtonDown("Fire1")) {
            anim.SetBool("LeftAtack", true);
        }
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log("fire2");
            anim.SetBool("RightAtack", true);
        }

        Debug.DrawRay(transform.position, dir, Color.red);
        if (Input.GetButtonDown("Interact") && routine == null) {
            Debug.Log("mov");
            routine = StartCoroutine(Drag());
        }
        if (Input.GetButtonUp("Interact")) {
            Drop();
        }
    }


    IEnumerator Climb() {
        RaycastHit hit, hit2;
        prepere = true;
        if (Physics.Raycast(transform.position + UpOfset, transform.forward, out hit, climbDistans) && (hit.transform.tag == "Climbable" || hit.transform.tag == "CliMov")) {
            if (Physics.Raycast(transform.position + UpOfset, -hit.normal, out hit2, climbDistans)) {
                hit = hit2;
            }
            Debug.Log("cli");
            Vector3 targetPos = hit.point + (hit.normal * climbDistans) - UpOfset;
            Vector3 begin = transform.position;
            Quaternion beg = transform.rotation;
            float betwen = 0;
            while (betwen < 1) {
                betwen += Time.deltaTime * prepereSpeed;
                transform.position = Vector3.Lerp(begin, targetPos, betwen);
                transform.rotation = Quaternion.Lerp(beg, Quaternion.LookRotation(-hit.normal), betwen);
                yield return null;
            }
            prepere = false;
            anim.SetTrigger("Climb");
        }
        routine = null;
    }
    IEnumerator Drag() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + UpOfset, transform.forward, out hit, maxDist) && (hit.transform.tag == "Movable" || hit.transform.tag == "CliMov")) {
            draging = true;
            Debug.Log("mov");
            Vector3 d = (hit.transform.position - UpOfset) - transform.position;
            d.Normalize();
            float df = Vector3.Dot(hit.transform.forward, d);
            float dr = Vector3.Dot(hit.transform.right, d);
            Vector3 rail;
            if (Mathf.Abs(df) > Mathf.Abs(dr)) {
                if (df < 0) {//forw
                    rail = hit.transform.forward;
                } else {//back
                    rail = -hit.transform.forward;
                }
            } else {
                if (dr < 0) {//right
                    rail = hit.transform.right;
                } else {//left
                    rail = -hit.transform.right;
                }
            }

            go = new GameObject();
            go.transform.position = hit.point;
            go.transform.parent = hit.transform;
            if (Physics.Raycast(transform.position + UpOfset, -rail, out hit, maxDist) && (hit.transform == go.transform.parent)) {
                go.transform.position = hit.point;
            }
            go.transform.position -= UpOfset;
            frez = true;
            prepere = true;
            anim.SetBool("Drag", true);
            Vector3 begin = transform.position;
            Quaternion beg = transform.rotation;
            float betwen = 0;
            Vector3 target = go.transform.position + (rail * armLength);
            while (betwen < 1) {
                betwen += Time.deltaTime * prepereSpeed;
                transform.position = Vector3.Lerp(begin, go.transform.position + (rail * armLength), betwen);
                transform.rotation = Quaternion.Lerp(beg, Quaternion.LookRotation(-rail), betwen);
                yield return null;
            }
            colider.center = new Vector3(0,4,-0.7f);
            while (true) {
                float differende = Vector3.Distance(go.transform.position, transform.position);
                go.transform.parent.GetComponent<Rigidbody>().velocity = transform.forward * (armLength - differende) * moc;
                dir = LevelForward * Input.GetAxis("Vertical") + Vector3.Cross(LevelForward, transform.up) * -Input.GetAxis("Horizontal");
                anim.SetFloat("Forward", Vector3.Dot(rail, -dir), 0.2f, Time.deltaTime);
                yield return null;
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (prepere) {
            Debug.Log(collision.gameObject.name);
            cl = collision.collider;
            Drop();
        }
    }
    private void OnCollisionStay(Collision collision) {
        if (collision.collider == cl) {
            Debug.Log("colstay");
            Drop();
            cl = null;
        }
        grounded = true;
    }
    private void OnCollisionExit(Collision collision) {
        grounded = false;
    }
    void Drop() {
        StopAllCoroutines();
        colider.center = new Vector3(0, 4, 0);
        anim.SetBool("Drag", false);
        draging = false;
        frez = false;
        prepere = false;
        if (go != null) {
            Destroy(go);
        }
    }
    public void LevelDir() {
        LevelForward = Vector3.ProjectOnPlane(cam.transform.forward, transform.up);
    }
    public void unfreez() {
        frez = false;
    }
    public void EndJump() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
