using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrzykładowyQuest : Quest {

    private void Start()
    {
        questName = "Przykladowy quest";
        questDescription = "Opis questa";
        //rozszerz tablicę o itemy rewards.Add();

        Goals.Add(new KillGoal(this,"szperaczBoss ", "Zabij jakiś cel", false, 0, 1));
        //Więcej celi 


        Goals.ForEach(g => g.Init());

    }
}
