using UnityEngine;
using UnityEditor;
using System.Xml;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{


    string fileLoadName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Dialogue dialogue = (Dialogue)target;

        GUILayout.Label("Enter you File Name");
        fileLoadName = EditorGUILayout.TextField(fileLoadName);


        GUILayout.BeginHorizontal();


        if (GUILayout.Button("Save"))
        {
            SaveDialogue(fileLoadName, dialogue);
        }

        if (GUILayout.Button("Load"))
        {
            LoadDialogue(fileLoadName, dialogue);
        }

        GUILayout.EndHorizontal();

    }

    public static void SaveDialogue(string fileLoadName, Dialogue speach)
    {
        Dialogue dialogue = speach;

        //int legnth = dialogue.pairSpeach.Length;

        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;

        // Create a write instance
        XmlWriter xmlWriter = XmlWriter.Create(fileLoadName + ".xml", writerSettings);
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();

        xmlWriter.WriteStartElement("DialogueFile");

        //xmlWriter.WriteAttributeString("length", legnth.ToString());

        for (int i = 0; i < dialogue.pairSpeach.Length; i++)
        {
            xmlWriter.WriteStartElement("pair");

            xmlWriter.WriteAttributeString("previousOption", dialogue.pairSpeach[i].previousOption.ToString());

            xmlWriter.WriteAttributeString("position", dialogue.pairSpeach[i].position.ToString());

            xmlWriter.WriteAttributeString("NPCSpeach", dialogue.pairSpeach[i].NPCSpeach.ToString());

            xmlWriter.WriteEndElement();

            

            for (int j = 0; j < 3; j++)
            {
                xmlWriter.WriteStartElement("pairResponse");
                xmlWriter.WriteAttributeString("playerResponse", dialogue.pairSpeach[i].playerResponse[j].ToString());

                xmlWriter.WriteAttributeString("branch", dialogue.pairSpeach[i].branch[j].ToString());
                xmlWriter.WriteEndElement();
            }

            
        }

        // End the root element
        xmlWriter.WriteEndElement();
        // Write the end of the document
        xmlWriter.WriteEndDocument();
        // Close the document to save
        xmlWriter.Close();
    }

    public static void LoadDialogue(string fileLoadName, Dialogue speach)
    {
        Dialogue dialogue = speach;

        int counter = 0;
        int counterResponse = 0;

        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileLoadName + ".xml");

        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {

            if (xmlReader.IsStartElement("DialogueFile"))
            {
                if (xmlReader.IsStartElement("pair"))
                {
                    dialogue.pairSpeach[counter].previousOption = int.Parse(xmlReader["previousOption"]);

                    dialogue.pairSpeach[counter].position = int.Parse(xmlReader["position"]);

                    dialogue.pairSpeach[counter].NPCSpeach = xmlReader["NPCSpeach"];

                    if (xmlReader.IsStartElement("pairResponse"))
                    {
                        dialogue.pairSpeach[counter].playerResponse[counterResponse] = xmlReader["playerResponse"];
                        dialogue.pairSpeach[counter].branch[counterResponse] = int.Parse(xmlReader["branch"]);

                        counterResponse++;
                    }
                    counter++;
                }
            }
        }
    }
}