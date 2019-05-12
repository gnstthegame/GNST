using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Quest : MonoBehaviour
{
    public List<Goal> Goals = new List<Goal>();
    public string questName { get; set; }
    public string questDescription { get; set; }
    public List<Item> rewards = new List<Item>();
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.goalCompleted);
        //if (Completed)
        //{
        //    GiveReward();
        //}
    }

    public void GiveReward()
    {
        if (rewards.Count != 0 || rewards != null)
        {
            //wyślij nagrodę do ekwipunku
        }
    }
    
}



































//[System.Serializable]
//public class Quest 
//{
//    public Status status;

//    public string title;
//    public string description;
//    public List<Item> reward;

//    public QuestGoal goal;


//    public void Complete()
//    {
//        status = Status.Completed;
//    }

//    public bool IsCompleted()
//    {
//        return goal.IsReached();
//    }

//    public void grantReward()
//    {
//        if (IsCompleted())
//        {
//            Complete();
//            foreach (Item item in reward)
//            {
//                //prześlij do inventory
//            }
//        }
//    }
//}

//public enum Status
//{
//    Inactive,
//    Active,
//    Completed,
//}
