﻿using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public bool isInRange = false;

    public virtual void Interact()
    {
        Debug.Log("Interakcja z: " + gameObject.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
