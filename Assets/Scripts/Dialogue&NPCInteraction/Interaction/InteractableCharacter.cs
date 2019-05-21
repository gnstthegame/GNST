using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour {

    public Interactable interactable;
    private bool interacted;

    private void Update()
    {
        if (interactable != null )
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactable = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit(Collider other)
    {
        interactable = null;
    }
}
