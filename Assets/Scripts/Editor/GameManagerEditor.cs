using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var manager = (GameManager)target;
        if (GUILayout.Button("Generate Random Name"))
        {
            manager.GenerateRandomNameInEditor();
        }

        if (GUILayout.Button("Generate Random Address"))
        {
            manager.GenerateRandomAddressInEditor();
        }

        if (!string.IsNullOrEmpty(manager.LastGeneratedName))
        {
            EditorGUILayout.HelpBox($"Last name: {manager.LastGeneratedName}", MessageType.Info);
        }

        if (!string.IsNullOrEmpty(manager.LastGeneratedAddress))
        {
            EditorGUILayout.HelpBox($"Last generated: {manager.LastGeneratedAddress}", MessageType.Info);
        }
    }
}
