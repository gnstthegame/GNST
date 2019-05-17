using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;

    public override void Interact()
    {
        //DialogueSystem.Instance.AddNewDialogue(dialogue, name);
        Debug.Log("Interakcja z NPC");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && isInRange)
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
}

