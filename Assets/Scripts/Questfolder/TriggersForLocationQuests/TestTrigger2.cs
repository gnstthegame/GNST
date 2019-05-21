using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger2 : Interactable {

    Transform playerTransform;
    bool isShown = false;
    bool isQuestGiver = false;
    string currentQuest;

    private void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    public override void Interact()
    {
        FindObjectOfType<ActiveQuests>().EnemyKilled("LoL");
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
