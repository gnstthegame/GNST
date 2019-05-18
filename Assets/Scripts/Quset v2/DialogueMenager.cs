using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMenager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;



    public Dialogue view;

    public Queue<Dialogue> sentenses; //

	// Use this for initialization
	void Start () {
        sentenses = new Queue<Dialogue>(); //
	}


  
   public void StartDialogue(TextAsset asset)
    {
        animator.SetBool("isOpen", true);

        DialogueContainer dc = DialogueContainer.Load(asset);

        sentenses.Clear();


        foreach (Dialogue item in dc.items)
        {
            sentenses.Enqueue(item);

        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if(sentenses.Count == 0)
        {
            EndDialogue();
            return;
        }



        Dialogue sentence = sentenses.Dequeue();
        dialogueText.text = sentence.text;
        nameText.text = sentence.characterName;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("isOpen", false);
        //FindObjectOfType<NPC>().IsShownOff();
    }
}
