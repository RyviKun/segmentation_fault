using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]
public class GridConfig : ScriptableObject
{
    public int width, height;
    public TileType[] layout;

    public void InitializeLayout()
    {
        layout = new TileType[width * height];
    }
}
