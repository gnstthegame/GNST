using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageReciver : MonoBehaviour {
    public Material mat;
    public bool asd = false;
    // Use this for initialization
    void Start () {
        mat = GetComponent<MeshRenderer>().material;
        //mat= GetComponent<Material>();


    }
	
	// Update is called once per frame
	void Update () {
        if (asd) {
            mat.color = Color.red;
        }
	}
    public void ApplyDamage(float dmg) {
        mat.color = Random.ColorHSV();
    }
}
