using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInArea : Interactable {

    Transform playerTransform;
    bool isShown = false;

    private void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    public override void Interact()
    {
        //if (!isShown)
        //{
        //isShown = true;
        this.GetComponent<DialogueTrigger>().TriggerDialogie();
        
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
    }


    public void IsShownOff()
    {
        isShown = false;
    }
}
