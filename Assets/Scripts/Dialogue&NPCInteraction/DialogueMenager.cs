using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueMenager : MonoBehaviour {

    CutsceneTrigger CT;
    public Text nameText;
    public Text dialogueText;
    //public Text nameText2;
    //public Text dialogueText2;
    //public Text button1;
    //public Text button2;
    //public Text button3;
    //public Text nameQuest;
    //public Text descriptionQuest;
    //public Text nameQuestFinnished;
    public GameObject panel;

    //public Animator animator;
    //public Animator animatorChoices;
    //public Animator animatorQuest;
    //public Animator animatorQuestFinnished;

    private string questID = null;

    public Dialogue view;

    public Queue<Dialogue> sentenses; //

	// Use this for initialization
	void Start () {
        sentenses = new Queue<Dialogue>(); //
	}
    private void Awake() {
        CT = FindObjectOfType<CutsceneTrigger>();
    }


    public void StartDialogue(TextAsset asset)
    {
        Debug.Log("czy wchodzę");
        //animator.SetBool("isOpen", true);
        FindObjectOfType<ToggleRendered>().ToggleVisibility(true);
        //GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<ToggleRendered>().ToggleVisibility(true);
        //panel.SetActive(true);


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
        CT.time = 0.1f;
        FindObjectOfType<PlayAudioWithID>().stopAudio();
        if(sentenses.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogue sentence = sentenses.Dequeue();

        if(sentence.mID != null)
        {
            FindObjectOfType<PlayAudioWithID>().playAudio(sentence.mID);
        }



        if(sentence.questTrigger != null)
        {


            Debug.Log("questID in DialogueMenager" + questID);
            //Quest quest = FindObjectOfType<QuestDatabase>().FindQuest(questID);
            questID = sentence.questTrigger;
            //GameObject.Find((FindObjectOfType<QuestDatabase>().FindQuest(questID)).character).GetComponent<NPC>().SetQuestGiverOn(questID);
            //FindObjectOfType<ActiveQuests>().AddToList(questID);
            Debug.Log("Rozpoczęto nowy quest");
            //if (nameQuest != null & descriptionQuest != null && animatorQuest != null)
            {
                //nameQuest.text = FindObjectOfType<QuestDatabase>().FindQuest(questID).questName;
                //descriptionQuest.text = FindObjectOfType<QuestDatabase>().FindQuest(questID).history;
                //animatorQuest.SetBool("isOpen", true);
            }

            //Debug.Log("mmmmmmmm" + FindObjectOfType<QuestDatabase>().FindQuest(questID).history);

            //if (quest.questName != null) { nameQuest.text = quest.questName; }
            //if (quest.history != null) { descriptionQuest.text = quest.history; }
        }



        //if (sentence.choices.Count != 0)
        //{
            //Debug.Log("aua");
            //animator.SetBool("isOpen", false);
            //if(animatorChoices != null){ 
            //animatorChoices.SetBool("isOpenC", true);
            //}

        //    if(sentence.choices.Count >= 1)
        //    {
        //        button1.text = sentence.choices[0].text;
        //    }

        //    if (sentence.choices.Count >= 2)
        //    {
        //        button2.text = sentence.choices[1].text;
        //    }

        //    if (sentence.choices.Count >= 3)
        //    {
        //        button3.text = sentence.choices[2].text;
        //    }

        //}
        //else
        //{
        //    animator.SetBool("isOpen", true);
        //    if (animatorChoices != null)
        //    {
        //        animatorChoices.SetBool("isOpenC", false);
        //    }
        //}

        Debug.Log(sentence.text);
        dialogueText.text = sentence.text;
        Debug.Log("mmmm" + dialogueText.text);
        nameText.text = sentence.characterName;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        FindObjectOfType<ToggleRendered>().ToggleVisibility(false);
        //panel.SetActive(false);
        //animator.SetBool("isOpen", false);
        //if (animatorChoices != null)
        //{
        //    animatorChoices.SetBool("isOpenC", false);
        //}
        //FindObjectOfType<NPC>().IsShownOff();
    }
    public void EndQuest()
    {
        //animatorQuest.SetBool("isOpen", false);
        //animatorQuestFinnished.SetBool("isOpen", false);
    }

    public void ShowQuest(string quest)
    {
        //nameQuestFinnished.text = " Zadanie " + quest + " zostało ukończone ";
        //animatorQuestFinnished.SetBool("isOpen", true);
    }
}
