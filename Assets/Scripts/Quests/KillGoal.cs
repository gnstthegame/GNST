using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal {
    public string EnemyName { get; set; } // albo inny identyfikator przeciwnika

    public KillGoal(Quest quest, string enemyName, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.EnemyName = enemyName;
        this.goalDescription = description;
        this.goalCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        //"dodać funkcja która wywołuje się w trakcie śmierci przeciwnika" += EnemyDied;
    }

    //void EnemyDied(any xxxx) //umieścić w klasie przeciwnika. gdy ginie przekazać tu tego przeciwnika
    //{
    //    if(xxxx == this.EnemyID)
    //    {
    //        currentAmount++;
    //        Evaluate();
    //    }
    //}

}
