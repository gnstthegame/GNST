using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAfterTimeInterval : Interactable {

    Transform playerTransform1;
    bool isShown = false;
    public float time;
    bool counter = false;

    private void Awake()
    {
        playerTransform1 = FindObjectOfType<CharacterMotor>().transform;
    }

    public override void Interact()
    {
            isShown = true;
            this.GetComponent<DialogueTrigger>().TriggerDialogueContinuosly();
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0.0f && counter == true) {
            Interact();
            //counter = false;
            time = 30.0f;
        }

        if(Input.GetButtonDown("Interact") && isInRange)
        {
            counter = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(counter == false)
        {
            Interact();
        }
        time = 30.0f;
        counter = true;
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }

    //public void IsShownOff()
    //{
    //    isShown = false;
    //}


}
