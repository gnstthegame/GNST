using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// aktywuje pole walki
/// </summary>
public class TurnOffTrigger : MonoBehaviour {
    public GameObject on, off;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            off.SetActive(false);
            off.SetActive(true);
        }
    }

}
