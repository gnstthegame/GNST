using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedDialogues : MonoBehaviour {

    public HashSet<int> usedDialogues;


    public void AddToUsedDialogues(int id)
    {
        usedDialogues.Add(id);
    }

    public bool IsUsedDialogue(int id)
    {
        if(usedDialogues.Contains(id) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
