using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridConfig))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ItemData itemData = (ItemData)target;
    }
}
