using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour {

    public List<TextAsset> QuestList;

    public Quest FindQuest(string ID)
    {
        foreach (TextAsset quest in QuestList)
        {
            string questID = quest.name.Substring(0, 3);
            if (questID == ID)
            {
                Debug.Log(" id questa " + ID);
                QuestContainer qc = QuestContainer.Load(quest);
                if (qc.quest.Count == 1)
                {
                    return qc.quest[0];
                }
                   

            }
        }
        return null;


    }
}
