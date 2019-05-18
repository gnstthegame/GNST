using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// obsługa poruszania się gracza
/// </summary>
public class CharacterMotor : MonoBehaviour {
    public float maxDist = 0.5f, armLength = 1f, prepereSpeed = 2, climbDistans = 1.5f;
    public float moc;
    public float BaseRotationSpeed = 10f;
    float RotationSpeed = 10f;
    public bool thirdP = false, saveLastPos=true;
    Animator anim;
    Vector3 LevelForward, dir = Vector3.forward;
    public bool frez = false, draging = false, prepere = false; //private
    Collider cl;
    CapsuleCollider colider;
    public Camera cam;
    Coroutine routine;
    GameObject go;
    [SerializeField]
    bool grounded = false;
    static Vector3 UpOfset = new Vector3(0, 0.2f, 0);
    Vector3[] lastPos = new Vector3[2] { Vector3.zero, Vector3.zero };
    int posIndex = 0;

    public GameObject triggeringNPC;
    public bool triggering;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "NPC") {
            triggering = true;
            triggeringNPC = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "NPC") {
            triggering = false;
            triggeringNPC = null;

        }
    }

    void Awake() {
        if (cam == null) {
            cam = Camera.main;
        }
        colider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        LevelDir();
        if (saveLastPos) {
            StartCoroutine(SaveFromFall());
        }
    }

    /// <summary>
    /// reaguje na wciśnięte klawisze
    /// </summary>
    void Update() {
        if (saveLastPos && transform.position.y < -5) {
            transform.position = lastPos[0];
        }
        if (thirdP) {
            LevelDir();
        }
        if (!draging) {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                float vert = Input.GetAxis("Vertical");
                float hor = Input.GetAxis("Horizontal");
                if (thirdP) {
                    RotationSpeed = BaseRotationSpeed / 5f;
                    if (Input.GetAxis("Vertical") < 0) {
                        anim.SetFloat("Forward", -1f, 0.1f, Time.deltaTime);
                    } else if (Input.GetAxis("Vertical") == 0) {
                        anim.SetFloat("Forward", 0, 0.2f, Time.deltaTime);
                    } else {
                        if (Input.GetButton("Sprint")) {
                            anim.SetFloat("Forward", 2f, 0.2f, Time.deltaTime);
                        } else {
                            anim.SetFloat("Forward", 1f, 0.05f, Time.deltaTime);
                        }
                    }
                } else {
                    RotationSpeed = BaseRotationSpeed;
                    if (Input.GetButton("Sprint")) {
                        anim.SetFloat("Forward", 2f, 0.2f, Time.deltaTime);
                    } else {
                        anim.SetFloat("Forward", 1f, 0.05f, Time.deltaTime);
                    }
                }
                dir = LevelForward * vert + Vector3.Cross(LevelForward, transform.up) * -hor;
                if (!frez) {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), RotationSpeed * Time.deltaTime);
                }
            } else {
                anim.SetFloat("Forward", 0, 0.2f, Time.deltaTime);
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

        Debug.DrawRay(transform.position, dir, Color.red);
        if (Input.GetButtonDown("Interact") && routine == null) {
            routine = StartCoroutine(Drag());
        }
        if (Input.GetButtonUp("Interact")) {
            Drop();
        }
    }

    /// <summary>
    /// rutyna wspinania się
    /// </summary>
    IEnumerator Climb() {
        frez = true;
        RaycastHit hit, hit2;
        prepere = true;
        if (Physics.Raycast(transform.position + UpOfset, transform.forward, out hit, maxDist) && (hit.transform.tag == "Climbable" || hit.transform.tag == "CliMov")) {
            if (Physics.Raycast(transform.position + UpOfset, -hit.normal, out hit2, maxDist)) {
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
        } else {
            anim.SetTrigger("jump");
        }
        routine = null;
    }
    /// <summary>
    /// rutyna przeciągania przedmiotów
    /// </summary>
    IEnumerator Drag() {
        RaycastHit hit;
        Debug.Log("mov");
        if (Physics.Raycast(transform.position + UpOfset, transform.forward, out hit, maxDist) && (hit.transform.tag == "Movable" || hit.transform.tag == "CliMov")) {
            draging = true;
            Debug.Log("Hited");
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
            colider.center = new Vector3(0, 4, -0.7f);
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
    /// <summary>
    /// przerwij przeciąganie obiektu
    /// </summary>
    void Drop() {
        Debug.Log("drop");
        StopAllCoroutines();
        //StopCoroutine(routine);
        routine = null;
        colider.center = new Vector3(0, 4, 0);
        anim.SetBool("Drag", false);
        draging = false;
        prepere = false;
        if (go != null) {
            Destroy(go);
        }
    }
    /// <summary>
    /// oblicz orientacje względem kamery
    /// </summary>
    public void LevelDir() {
        LevelForward = Vector3.ProjectOnPlane(cam.transform.forward, transform.up);
    }
    /// <summary>
    /// odblokuj rotacje
    /// </summary>
    public void unfreez() {
        frez = false;
    }
    /// <summary>
    /// koniec skoku
    /// </summary>
    public void EndJump() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    /// <summary>
    /// rutyna zapisująca ostatnią bezpieczną pozycję gracza
    /// </summary>
    /// <returns></returns>
    IEnumerator SaveFromFall() {
        while (true) {
            lastPos[0] = new Vector3(lastPos[1].x, lastPos[1].y, lastPos[1].z);
            lastPos[1] = transform.position;
            yield return new WaitForSeconds(5f);
        }
    }

}
