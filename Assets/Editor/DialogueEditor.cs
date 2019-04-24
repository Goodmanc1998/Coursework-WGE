using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{


    int branchLength;

    int[] branch;

    string[] NPCSpeach;
    string[] playerText;
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Dialogue dialogue = (Dialogue)target;

        GUILayout.BeginHorizontal();

        branchLength = EditorGUILayout.IntField("How long is this branch?", branchLength);

        if (GUILayout.Button("Start Branch"))
        {
            branch = new int[branchLength];

            NewBranch(null,null,null, branchLength);

        }

        GUILayout.EndHorizontal();

    }


    public static string NewBranch(string firstResponse, string[] NPCSpeach, string[] playerResponse, int branchLength)
    {

        firstResponse = EditorGUILayout.TextField(firstResponse);

        NPCSpeach = new string[branchLength];
        playerResponse = new string[branchLength];

        for (int i = 0; i < branchLength; i++)
        {
            NPCSpeach[i] = EditorGUILayout.TextField(NPCSpeach[i]);
            playerResponse[i] = EditorGUILayout.TextField(playerResponse[i]);
        }

        return null;
    }
}
