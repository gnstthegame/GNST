using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    internal CharacterMotor player;
    protected bool hasInteracted;
    public string name;
    private void OnValidate() {
        player = FindObjectOfType<CharacterMotor>();
    }

    private void Update()
    {
        if (player.triggering == true && name == player.triggeringNPC.name)
        {

            if (hasInteracted == false)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }
        if (player.triggering == false || name != player.triggeringNPC.name)
        {
            hasInteracted = false;
        }

    }


    public virtual void Interact()
    {
        Debug.Log("Interakcja z klasą bazową");
    }

}
