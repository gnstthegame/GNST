using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFLQ207 : Interactable {

    Transform playerTransform;
    bool visited = false;

    private void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    public override void Interact()
    {
        FindObjectOfType<ActiveQuests>().PlaceVisited("207");
        visited = true;
    }

    private void Update()
    {
        if (isInRange && FindObjectOfType<ActiveQuests>().IsActive("207") && !visited)
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
