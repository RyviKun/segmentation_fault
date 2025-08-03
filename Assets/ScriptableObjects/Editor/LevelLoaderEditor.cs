using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelLoader))]
public class LevelLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelLoader itemData = (LevelLoader)target;
    }
}
