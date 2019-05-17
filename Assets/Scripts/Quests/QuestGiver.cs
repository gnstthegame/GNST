//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class QuestGiver : NPC
//{
//    public bool AssignedQuest { get; set; }
//    public bool Helped { get; set; }

//    [SerializeField]
//    private GameObject quests;

//    [SerializeField]
//    private string questType;
//    private Quest quest;

//    public override void Interact()
//    {

//        if (!AssignedQuest && !Helped)
//        {
//            base.Interact();
//            AssignQuest();
//        }
//        else if (AssignedQuest && !Helped)
//        {
//            CheckQuest();
//        }
//        else
//        {
//            DialogueSystem.Instance.AddNewDialogue(new string[] { "Nie mam dla ciebie wiecej zadan" }, name);
//        }
//    }

//    void AssignQuest()
//    {
//        AssignedQuest = true;
//        quest = quests.AddComponent(System.Type.GetType(questType)) as Quest;
//    }

//    void CheckQuest()
//    {
//        if (quest.Completed)
//        {
//            quest.GiveReward();
//            Helped = true;
//            AssignedQuest = false;
//            DialogueSystem.Instance.AddNewDialogue(new string[] { "Dzieki za wykonianie zadania" }, name);
//        }
//        else
//        {
//            DialogueSystem.Instance.AddNewDialogue(new string[] { "Wyglada na to, ze musisz jeszcze ukonczyc zadanie" }, name);
//        }
//    }
//}


