using UnityEngine;
using UnityEditor;
using System.Xml;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    //Storing the filename
    string fileLoadName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Targetting the Dialogue script
        Dialogue dialogue = (Dialogue)target;

        GUILayout.Label("Enter you File Name");
        fileLoadName = EditorGUILayout.TextField(fileLoadName);

        //Below to be displayed in horizontal
        GUILayout.BeginHorizontal();
        //Save and load button which calls seperate save and load methods
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

        XmlWriterSettings writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;

        // Create a write instance
        XmlWriter xmlWriter = XmlWriter.Create(fileLoadName + ".xml", writerSettings);
        // Write the beginning of the document
        xmlWriter.WriteStartDocument();
        
        //Creating the root
        xmlWriter.WriteStartElement("DialogueFile");

        //Loops for the pairSpeach length
        for (int i = 0; i < dialogue.pairSpeach.Length; i++)
        {
            // Create a single pair element
            xmlWriter.WriteStartElement("pair");

            // Write an attribute to store the previousOption
            xmlWriter.WriteAttributeString("previousOption", dialogue.pairSpeach[i].previousOption.ToString());
            // Write an attribute to store the position
            xmlWriter.WriteAttributeString("position", dialogue.pairSpeach[i].position.ToString());
            // Write an attribute to store the NPCSPeach
            xmlWriter.WriteAttributeString("NPCSpeach", dialogue.pairSpeach[i].NPCSpeach.ToString());

            // Ending the pair element
            xmlWriter.WriteEndElement();

            for (int j = 0; j < 3; j++)
            {
                //create a single pairResponse Element
                xmlWriter.WriteStartElement("pairResponse");
                // Write an attribute to store the playerResponse
                xmlWriter.WriteAttributeString("playerResponse", dialogue.pairSpeach[i].playerResponse[j].ToString());
                // Write an attribute to store the branch
                xmlWriter.WriteAttributeString("branch", dialogue.pairSpeach[i].branch[j].ToString());
                //Ending the pairResponse Element
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

        //Setting up counters to access correct pairSpeach variables
        int counter = 0;
        int counterResponse = 0;

        // Create an XML reader with the file supplied
        XmlReader xmlReader = XmlReader.Create(fileLoadName + ".xml");

        // Iterate through and read every line in the XML file
        while (xmlReader.Read())
        {
            // Check if this node is a pair element
            if (xmlReader.IsStartElement("pair"))
            {
                // Retrieve previousOption attribute and store as int
                dialogue.pairSpeach[counter].previousOption = int.Parse(xmlReader["previousOption"]);
                // Retrieve position attribute and store as int
                dialogue.pairSpeach[counter].position = int.Parse(xmlReader["position"]);
                // Retrieve NPCSpeach attribute and store as string
                dialogue.pairSpeach[counter].NPCSpeach = xmlReader["NPCSpeach"];

                if (xmlReader.IsStartElement("pairResponse"))
                {
                    // Retrieve playerResponse attribute and store as string
                    dialogue.pairSpeach[counter].playerResponse[counterResponse] = xmlReader["playerResponse"];
                    // Retrieve branch attribute and store as int
                    dialogue.pairSpeach[counter].branch[counterResponse] = int.Parse(xmlReader["branch"]);
                    //Updating Counter
                    counterResponse++;
                }
                //Updating Counter
                counter++;
            }
            //Resetting the counter
            counterResponse = 0;
        }
    }
}