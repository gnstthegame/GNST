using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    List<GameObject> hited= new List<GameObject>();
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Enemy" && hited.Contains(collision.gameObject) == false) {
            collision.gameObject.SendMessage("ApplyDamage", 10, SendMessageOptions.DontRequireReceiver);
            hited.Add(collision.gameObject);
        }
    }
    public void Rese() {
        hited.Clear();
    }
}
