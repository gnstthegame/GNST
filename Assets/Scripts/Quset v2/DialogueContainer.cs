using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DialogueCollection")]
public class DialogueContainer{

    [XmlArray("Dialogues"), XmlArrayItem("Dialogue")]
    public List<Dialogue> items = new List<Dialogue>();

    public static DialogueContainer Load(TextAsset asset)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DialogueContainer));

        StringReader reader = new StringReader(asset.text);

        DialogueContainer dialogue = serializer.Deserialize(reader) as DialogueContainer;

        reader.Close();

        return dialogue;


    }
}
