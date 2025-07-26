using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridConfig))]
public class GridConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
            
        GridConfig config = (GridConfig)target;

        if (GUILayout.Button("parse Layout"))
        {
            config.ParseLayout();
            EditorUtility.SetDirty(config); // marks it dirty so Unity saves it
        }
    }
}