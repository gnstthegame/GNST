using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class DialogueCollection {

    public List<TextAsset> xmlFiles;
}


[System.Serializable]
public class Dialogue
{
    [XmlAttribute("DialogueID")]
    public int DialogueID;
    [XmlElement("CharacterName")]
    public string characterName;
    [XmlElement("text")]
    public string text;
    [XmlElement("moduleID")]
    public string mID;
    [XmlElement("questTrigger")]
    public string questTrigger;
    [XmlElement("isDisabled")]
    public bool isDisabled = true;
    [XmlElement("choices")]
    public List<Choices> choices;
}

[System.Serializable]
public class Choices
{

    [XmlElement("text")]
    public string text;
    [XmlElement("nextDialogueID")]
    public int nextDialogueID;
}
