using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : Goal
{
    public Item targetItem;

    public GatherGoal(Quest quest, Item item, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.targetItem = item; 
        this.goalDescription = description;
        this.goalCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        //"dodać funkcja która wywołuje się przy dodaniu przedmiotu do ekwipunku" += ItemPickedUp;
    }

    void ItemPickedUp(Item item)
    {
        if(item == this.targetItem)
        {
            currentAmount++;
            Evaluate();
        }
    }

}
