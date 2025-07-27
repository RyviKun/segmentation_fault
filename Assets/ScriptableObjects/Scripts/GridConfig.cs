using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]
public class GridConfig : ScriptableObject
{
    public int width, height;
    public TileData[] layout;
    [TextArea]
    public string layoutString;


    public void InitializeLayout()
    {
        layout = new TileData[width * height];
    }

    public void ParseLayout()
    {
        string cleaned = layoutString.Replace(" ", "")
                                  .Replace("\n", "")
                                  .Replace("\r", "")
                                  .Replace("\t", "");
        layout = new TileData[width * height];
        for (int i = 0; i < cleaned.Length ; i++)
        {
            char c = cleaned[i];
            layout[i] = new TileData(CharToTileData(c), 0);
        }
    }

    private TileType CharToTileData(char c)
    {
        return c switch
        {
            '0' => TileType.Empty,
            '1' => TileType.SpawnPoint,
            '2' => TileType.Wall,
            '3' => TileType.Enemy
        };
    }
}
