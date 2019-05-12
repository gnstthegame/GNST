using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public static DialogueSystem Instance { get; set; }

    public GameObject dialoguePanel;


    public List<string> dialogueLines = new List<string>();
    string npcName;

    Button continueButton;
    Text dialogueTextfield;
    Text nameTextfield;
    int dialogueIndex;


    void Awake () {
        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        dialogueTextfield = dialoguePanel.transform.Find("Text").GetComponent<Text>();
        nameTextfield = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>(); 
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}


	
	public void AddNewDialogue(string[] lines, string name)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        npcName = name;

        CreateDialogue();

    }

    public void CreateDialogue()
    {
        dialogueTextfield.text = dialogueLines[dialogueIndex];
        nameTextfield.text = npcName;
        Debug.Log("testtesttest");
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
       if( dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueTextfield.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
