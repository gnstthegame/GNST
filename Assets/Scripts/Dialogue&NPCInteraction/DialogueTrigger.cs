using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public DialogueCollection dialogue;

    public int i = 0;

    public void TriggerDialogie ()
    {
        Debug.Log(gameObject.name);
        if (dialogue.xmlFiles.Count != 0)
        {
            TextAsset asset = dialogue.xmlFiles[0];
            dialogue.xmlFiles.RemoveAt(0);
            FindObjectOfType<DialogueMenager>().StartDialogue(asset);
        }

    }

    public void TriggerDialogueContinuosly()
    {
        if (dialogue.xmlFiles.Count != 0)
        {
            TextAsset asset = dialogue.xmlFiles[i];
            FindObjectOfType<DialogueMenager>().StartDialogue(asset);
            i = (i + 1) % dialogue.xmlFiles.Count;
        }

    }

    //public void TriggerDialogie(int id)
    //{
    //    for (int i = 0; i < dialogue.xmlFiles.Count; i++)
    //    {
    //        DialogueContainer dc = DialogueContainer.Load(dialogue.xmlFiles[i]);
    //        if (dc.items.Count != 0)
    //        {
    //            Dialogue tmp = dc.items[0];
    //            if (tmp.DialogueID == id)
    //            {
    //                TextAsset asset = dialogue.xmlFiles[i];
    //                dialogue.xmlFiles.RemoveAt(i);
    //                FindObjectOfType<DialogueMenager>().StartDialogue(asset);
    //            }
    //        }
    //    }
    //}

    //public void AddToPastDialoguesList()
    //{

    //}

}
