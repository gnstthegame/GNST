using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public GameObject Object;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Object.SetActive(true);
            Destroy(gameObject);
        }
    }

}
