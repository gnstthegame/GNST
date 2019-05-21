using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

    public List<TextAsset> questList;

}

[System.Serializable]
public class Quest
{
    [XmlAttribute("QuestId")]
    public string QuestID;
    [XmlElement("name")]
    public string questName; 
    [XmlElement("history")]
    public string history;//opis questa
    [XmlElement("character")]
    public string character; //kto daje questa
    [XmlElement("type")]
    public string type;
    [XmlElement("target")]
    public string target;
    [XmlElement("current")]
    public int currentValue;
    [XmlElement("needed")]
    public int valueNeeded;
    [XmlElement("enableDialogues")]
    public List<int> enableDialogues;
    [XmlElement("items")]
    public List<Items> items; //co można dostać za questa
}

[System.Serializable]
public class Items
{
    [XmlAttribute("id")]
    public string ItemID;
    [XmlElement("text")]
    public string text;
}

//public enum QuestType
//{
//    [XmlEnum("K")]Kill,
//    [XmlEnum("G")]Gather
//}