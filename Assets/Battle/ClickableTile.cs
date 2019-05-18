using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// klasa interaktywnych pól bojowych
/// </summary>
public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    int stat = 0;
    public int type = 0;//0-normal, 1-avoid, 2-blocked
    public Color walkable = new Color(1f, 1f, 1f, 0.2f), pass = new Color(0.7f, 0.7f, 1f, 0.8f), select = new Color(0f, 0f, 1f, 0.8f), far = new Color(0f, 0.0f, 0f, 0.2f), direct = new Color(1f, 0.5f, 0f, 0.8f), atack = new Color(1f, 0f, 0f, 0.8f);
    Renderer Re;
    public Unit Unit = null;
    /// <summary>
    /// przypisuje startowe komponenty
    /// </summary>
    private void Awake() {
        Re = GetComponent<Renderer>();
        Re.material.SetColor("_Color", far);
    }
    /// <summary>
    /// inicjuje dzia³anie obiektu
    /// </summary>
    public void Init() {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(transform.localScale.x / 2.1f, 0f, transform.localScale.z / 2.1f));
        List<Collider> col = new List<Collider>(hitColliders);
        col.RemoveAll(item => item.isTrigger || item.tag == "Player" || item.tag == "Enemy");
        if (col.Count != 0) {
            type = 3;
        }
    }
    /// <summary>
    /// zapisuje status pola i zmienia jego kolor
    /// </summary>
    /// <param name="s">status</param>
    public void Stat(int s) {
        stat = s;
        switch (s) {
            case 0://far
                Re.material.color = far;
                break;
            case 1://clear
                Re.material.color = walkable;
                break;
            case 2://pass by
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
    /// <summary>
    /// oblicza koszt wejœcia na pole
    /// </summary>
    /// <param name="ignoreOcc">ignorowaæ inne jednostki</param>
    /// <returns>koszt</returns>
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
                return Mathf.Infinity;
            case 3://Blocked
                return Mathf.Infinity;
            default:
                return 1;
        }
    }
    /// <summary>
    /// reakcja na najechanie mysz¹
    /// </summary>
    public void MouseEnter() {
        if (stat == 1 || stat == 2) {
            map.ViewPath(tileX, tileY);
        }
        if (stat == 4) {
            map.MarkAtack(tileX, tileY);
        } else {
            Re.material.color = select;
        }
    }
    /// <summary>
    /// reakcja na zjechanie mysz¹
    /// </summary>
    public void MouseExit() {
        Stat(stat);
        if (stat == 5) {
            map.AtackMode();
        }
    }
    /// <summary>
    /// reakcja na przycisk myszy
    /// </summary>
    public void MouseDown() {
        map.TileClicked(tileX, tileY);
    }
}
