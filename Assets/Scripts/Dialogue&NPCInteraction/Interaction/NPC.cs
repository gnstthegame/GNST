using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    Transform playerTransform;
    bool isShown = false;
    //public string name;
    public bool  isQuestGiver = false;
    string currentQuest;

    private void Awake()
    {
        playerTransform = FindObjectOfType<CharacterMotor>().transform;
    }

    public override void Interact()
    {
        if (isQuestGiver == false)
        {
            if (GetComponent<DialogueTrigger>())
            {
                this.GetComponent<DialogueTrigger>().TriggerDialogie();
            }
        
        }
        else
        {
            if (FindObjectOfType<ActiveQuests>().IsCompleted(currentQuest) == true)
            {
                Debug.Log("Okno ukończenia questa");
                FindObjectOfType<ActiveQuests>().GetReward(currentQuest);
                if (GetComponent<DialogueTrigger>())
                {
                    this.GetComponent<DialogueTrigger>().TriggerDialogie();
                }
            }
            else
            {
                Debug.Log("Musisz się jeszcze postarać");
            }
        }
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

    public void IsShownOff()
    {
        isShown = false;
    }

    public void SetQuestGiverOn(string questID)
    {
        currentQuest = questID;
        isQuestGiver = true;
    }

    public void SetQuestGiverOff()
    {
        isQuestGiver = false;
    }
}

