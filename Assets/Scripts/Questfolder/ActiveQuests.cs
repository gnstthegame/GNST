using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuests : MonoBehaviour {

    public List<Quest> ActiveQuestsList;


    public bool AddToList(string ID)
    {
        Quest quest = FindObjectOfType<QuestDatabase>().FindQuest(ID);
        if (quest != null)
        {
            ActiveQuestsList.Add(quest);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveFromList(string questID)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (quest.QuestID == questID)
            {
                ActiveQuestsList.Remove(quest);
            }

        }
    }


    public bool IsActive(string ID)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (quest.QuestID == ID)
            {
                return true;
            }
        }
        return false;
    }

    //public int UpdateQuest(string questID, int currentValue)
    //{
    //    foreach (Quest quest in ActiveQuestsList)
    //    {
    //        if(questID == quest.QuestID)
    //        {
    //            quest.currentValue = currentValue;

    //            return 1;
    //        }
    //    }

    //    if (AddToList(questID) == true){
    //        ActiveQuestsList[ActiveQuestsList.Count - 1].currentValue = currentValue;
    //    }
    //    return 0;
    //}


    public void RemoveFromList(Quest quest)
    {
        ActiveQuestsList.Remove(quest);
    }

    public List<Quest> GetActiveQuests()
    {
        return ActiveQuestsList;
    }

    public void EnemyKilled(string enemyName)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (quest.type == "Kill" && quest.target == enemyName)
            {
                quest.currentValue++;
                Debug.Log("Zabito przeciwnika");
            }
        }
    }

    public void ItemCollected(string itemName)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (quest.type == "Gather" && quest.target == itemName)
            {
                quest.currentValue++;
            }
        }
    }

    public void PlaceVisited(string questID)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (quest.type == "Location" && quest.target == questID)
            {
                quest.currentValue++;
            }
        }
    }

    public bool IsCompleted(string questID)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (questID == quest.QuestID && quest.currentValue >= quest.valueNeeded)
            {
                Debug.Log("Quest " + quest.questName + " ukończony");
                return true;
            }
        }
        Debug.Log("Quest nieukończony");
        return false;
    }

    public void GetReward(string questID)
    {
        foreach (Quest quest in ActiveQuestsList)
        {
            if (questID == quest.QuestID && quest.currentValue >= quest.valueNeeded)
            {
                foreach (Items item in quest.items)
                {
                    FindObjectOfType<RewardController>().SendReward(item.ItemID);
                }
                Debug.Log("Nagroda przyznana");

                FindObjectOfType<DialogueMenager>().ShowQuest(FindObjectOfType<QuestDatabase>().FindQuest(questID).questName);

                ActiveQuestsList.Remove(quest);
                break;
            }
        }
    }

}


public class ActiveQuestsSave{

    public string QuestID;
    public int currentValue;


}
