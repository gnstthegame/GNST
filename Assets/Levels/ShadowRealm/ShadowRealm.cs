using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRealm : MonoBehaviour {
    public int width = 5, variationW = 2;
    public int langth = 5, variationL = 2;
    public int segments = 3;
    public float scaleX = 8, scaleY = 8;
    public GameObject Prefab0, Prefab1, Prefab2, szafa;
    public Transform Player;
    GameObject szaf;
    List<int> GSym = new List<int>(), BSym = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int y = 0, x = 0, seg = 1;
    List<GameObject[]> way = new List<GameObject[]>(), left = new List<GameObject[]>(), right = new List<GameObject[]>(), decoy = new List<GameObject[]>(), good = new List<GameObject[]>();
    FallingTrigger triger;
    bool win = false;

    void Awake() {
        triger = Player.GetComponent<FallingTrigger>();
        for (int i = 0; i < segments && i < 5; i++) {
            int sym = BSym[Random.Range(0, BSym.Count)];
            GSym.Add(sym);
            BSym.Remove(sym);
        }
        y = makePath(0, x, good);
        int ry = Random.Range(1, good.Count);
        int rx = Random.Range(1, good[ry].Length - 1);
        good[ry][rx].SendMessage("Mark", GSym[0] + 3, SendMessageOptions.DontRequireReceiver);

        Build();
        //Restart();
    }
    void Restart() {
        DestroyList(decoy);
        DestroyList(way);
        DestroyList(good);
        good = new List<GameObject[]>();
        GSym = new List<int>();
        BSym = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        for (int i = 0; i < segments && i < 5; i++) {
            int sym = BSym[Random.Range(0, BSym.Count)];
            GSym.Add(sym);
            BSym.Remove(sym);
        }
        x = 0; seg = 1;
        y = makePath(0, x, good);
        int ry = Random.Range(1, good.Count);
        int rx = Random.Range(1, good[ry].Length - 1);
        good[ry][rx].SendMessage("Mark", GSym[0] + 3, SendMessageOptions.RequireReceiver);
        Debug.Log(ry + " " + rx);
        Build();
        Player.position = new Vector3(way[0].Length / 2 * scaleX, 30f, 2f);
        triger.Contin();

    }

    void Build() {
        DestroyList(decoy);
        DestroyList(way);
        left = new List<GameObject[]>();
        right = new List<GameObject[]>();
        way = new List<GameObject[]>(good);
        good = new List<GameObject[]>();

        y = rozwidlenie(y, x);
        int ry, rx;

        int r = Random.Range(0, 2) * 2 - 1;
        if (r < 0) {
            good = left;
            decoy = right;
        } else {
            good = right;
            decoy = left;
        }
        int yy = good.Count;
        makePath(y, x - r * 4, decoy);
        y = makePath(y, x + r * 4, good);
        x += r * 4;

        int s = seg < 5 ? Random.Range(0, seg) : Random.Range(0, 4);
        ry = Random.Range(yy - 3, yy);
        rx = Random.Range(1, good[ry].Length - 1);
        good[ry][rx].SendMessage("Mark", GSym[s] + 3, SendMessageOptions.DontRequireReceiver);

        s = seg < 5 ? Random.Range(0, seg) : Random.Range(0, 4);
        ry = Random.Range(yy - 3, yy);
        rx = Random.Range(1, decoy[ry].Length - 1);
        decoy[ry][rx].SendMessage("Mark", BSym[s] + 3, SendMessageOptions.DontRequireReceiver);

        seg++;
        s = seg < 5 ? seg - 1 : Random.Range(0, 4);
        ry = Random.Range(yy + 1, good.Count);
        rx = Random.Range(1, good[ry].Length - 1);
        good[ry][rx].SendMessage("Mark", GSym[s] + 3, SendMessageOptions.DontRequireReceiver);

        s = seg < 5 ? seg - 1 : Random.Range(0, 4);
        ry = Random.Range(yy + 1, decoy.Count);
        //Debug.Log(ry + " " + seg + " " + s + " " + yy + " " + y + " " + x + " " + r);
        rx = Random.Range(1, decoy[ry].Length - 1);
        decoy[ry][rx].SendMessage("Mark", BSym[s] + 3, SendMessageOptions.DontRequireReceiver);
        if (seg == segments) {
            szaf = (GameObject)Instantiate(szafa, new Vector3(((good[good.Count - 1].Length) / 2 + x) * scaleX, szafa.transform.localScale.y / 2, y * scaleY) + transform.position, Quaternion.identity);
        }
    }

    void Update() {
        if (Mathf.Abs(y * scaleY - Player.position.z) < 16f && Mathf.Abs((x+2) * scaleX - Player.position.x) < 16f && !win) {
            if (seg < segments) {
                Debug.Log("bild");
                Build();
                triger.Contin();
            } else {
                Debug.Log("win");
                triger.Win();
                DestroyList(decoy);
                DestroyList(way);
                
                win = true;
            }
        }
        if (Player.position.y < -20f) {
            Destroy(szaf);
            Restart();
        }

    }
    void DestroyList(List<GameObject[]> tmp) {
        foreach (var g in tmp) {
            foreach (var h in g) {
                Destroy(h);
            }
        }
        tmp = new List<GameObject[]>();
    }

    int makePath(int y, float xoffset, List<GameObject[]> tmp) {
        int maxL = langth + y + Random.Range(0, variationL + 1);
        for (; y < maxL; y++) {
            int vari = Random.Range(0, variationW + 1);
            GameObject[] temp = new GameObject[width + vari];
            float push = Random.value - 0.5f;
            int maxW = vari + width;
            for (int x = 0; x < maxW; x++) {
                GameObject go = (GameObject)Instantiate(randomBrick(), new Vector3((x + xoffset - ((float)vari + push) / 2) * scaleX, scaleX / 8, y * scaleY) + transform.position, Quaternion.identity, transform);
                if (x == 0) {
                    go.SendMessage("Mark", 0, SendMessageOptions.DontRequireReceiver);
                }
                if (x == maxW - 1) {
                    go.SendMessage("Mark", 2, SendMessageOptions.DontRequireReceiver);
                }
                temp[x] = go;
            }
            tmp.Add(temp);
        }
        return y;
    }
    GameObject randomBrick() {
        switch (Random.Range(0, 3)) {
            case (1):
                return Prefab1;
            case (2):
                return Prefab2;
            default:
                return Prefab0;
        }
    }
    int rozwidlenie(int y, float xoffset) {
        int maxL = langth + variationL + y;
        int maxW = width;
        int vars = 0;
        for (; y < maxL; y++) {
            vars++;
            float push = Random.value * 2 - 1f;
            GameObject[] temp = new GameObject[maxW + vars];
            for (int x = 0; x < maxW + vars; x++) {
                GameObject go = (GameObject)Instantiate(randomBrick(), new Vector3((x + xoffset - ((float)vars + push) / 2) * scaleX, scaleX / 8, y * scaleY) + transform.position, Quaternion.identity, transform);
                if (x == 0) {
                    go.SendMessage("Mark", 0, SendMessageOptions.DontRequireReceiver);
                }
                if (x == maxW + vars - 1) {
                    go.SendMessage("Mark", 2, SendMessageOptions.DontRequireReceiver);
                }
                if (y == maxL - 1 && (x == (maxW + vars) / 2 + 1 || x == (maxW + vars) / 2 || x == (maxW + vars) / 2 - 1 || x == (maxW + vars) / 2 - 2)) {
                    go.SendMessage("Mark", 1, SendMessageOptions.DontRequireReceiver);
                }
                temp[x] = go;
            }
            if (y < 3) {
                way.Add(temp);
            } else {
                int p = (maxW + vars) % 2;
                int half = (maxW + vars) / 2 + p;
                GameObject[] l = new GameObject[half];
                GameObject[] r = new GameObject[half - p];
                for (int i = 0; i < half; i++) {
                    l[i] = temp[i];
                }
                for (int i = 0; i < half - p; i++) {
                    r[i] = temp[i + half];
                }
                left.Add(l);
                right.Add(r);
            }
        }
        return y;
    }


}
