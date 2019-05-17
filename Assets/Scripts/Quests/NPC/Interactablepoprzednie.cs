using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactablee : MonoBehaviour {

    internal CharacterMotor player;
    protected bool hasInteracted;
    public string name;
    private void Awake() {
        player = FindObjectOfType<CharacterMotor>();
    }

    private void Update()
    { 

    }


    public virtual void Interact()
    {
        Debug.Log("Interakcja z klasą bazową");
    }

}
