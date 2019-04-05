using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRealm : MonoBehaviour {
    public int width = 5, variationW = 2;
    public int langth = 5, variationL = 2;
    public int segments = 3;
    public float scaleX = 8, scaleY = 8;
    public GameObject Prefab0,Prefab1, Prefab2;
    public Transform Player;

    // Use this for initialization
    void Awake() {
        int y = makePath(0, 0);
        y = rozwidlenie(y);
        makePath(y, -4);
        y = makePath(y, 4);
    }
    int makePath(int y, float xoffset) {
        int maxL = langth + y;
        for (; y < maxL; y++) {
            int vari = Random.Range(0, variationW + 1);
            float push = Random.value - 0.5f;
            int maxW = vari + width;
            for (int x = 0; x < maxW; x++) {
                GameObject go = (GameObject)Instantiate(randomBrick(), new Vector3((x + xoffset - ((float)vari + push) / 2) * scaleX, scaleX / 8, y * scaleY) + transform.position, Quaternion.identity, transform);
                ClickableTile ct = go.GetComponent<ClickableTile>();
                //player
            }
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
    int rozwidlenie(int y) {
        int maxL = langth + variationL + y;
        int maxW = width;
        int vars=0;
        for (; y < maxL; y++) {
            //int vari = Random.Range(0, variationW + 1);
            vars++;
            float push = Random.value*2 - 1f;
            for (int x = 0; x < maxW + vars; x++) {
                GameObject go = (GameObject)Instantiate(randomBrick(), new Vector3((x - ((float)vars + push) / 2) * scaleX, scaleX / 8, y * scaleY) + transform.position, Quaternion.identity, transform);
        }
    }
        return y;
    }


// Update is called once per frame
void Update() {

    }
}
