using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    int stat = 0;
    public int type = 0;//0-normal, 1-avoid, 2-blocked
    public Color walkable = new Color(1f, 1f, 1f, 0.2f), pass = new Color(0.7f, 0.7f, 1f, 0.8f), select = new Color(0f, 0f, 1f, 0.8f), far = new Color(0f, 0.0f, 0f, 0.2f), direct = new Color(1f, 0.5f, 0f, 0.8f), atack = new Color(1f, 0f, 0f, 0.8f);
    Renderer Re;
    public Unit Unit = null;

    private void Awake() {
        Re = GetComponent<Renderer>();
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(transform.localScale.x / 2.1f, 0f, transform.localScale.z / 2.1f));
        if (hitColliders.Length != 1) {
            type = 3;
        }
        Re.material.SetColor("_Color", far);
    }
    private void Update() {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 15);
    }
    public void Stat(int s) {
        stat = s;
        switch (s) {
            case 0://far
                Re.material.color = far;
                break;
            case 1://clear
                Re.material.color = walkable;
                break;
            case 2://pass
                Re.material.color = pass;
                break;
            case 3://select
                Re.material.color = select;
                break;
            case 4://direction for atack
                Re.material.color = direct;
                break;
            case 5://atack area
                Re.material.color = atack;
                break;
            default://far
                Re.material.color = far;
                break;
        }
    }
    public float MovingCost(bool ignoreOcc = false) {
        switch (type) {
            case 0://normal
                if (Unit == null || ignoreOcc) {
                    return 1;
                } else {
                    return Mathf.Infinity;
                }
            case 1://avoid
                return 1.001f;
            case 2://occupied
            case 3://Blocked
                return Mathf.Infinity;
            default:
                return 1;
        }
    }
    void OnMouseEnter() {
        if (stat == 1 || stat == 2) {
            map.ViewPath(tileX, tileY);
        }
        if (stat == 4) {
            map.MarkAtack(tileX, tileY);
        } else {
            Re.material.color = select;
        }
    }
    void OnMouseExit() {
        Stat(stat);
        if (stat == 5) {
            map.AtackMode();
        }
    }
    void OnMouseUp() {
    }
    void OnMouseDown() {
        map.TileClicked(tileX, tileY);
    }
}
