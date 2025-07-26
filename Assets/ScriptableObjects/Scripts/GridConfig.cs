using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]
public class GridConfig : ScriptableObject
{
    public int width, height;
    public TileType[] layout;
    [TextArea]
    public string layoutString = "100011100"; // You enter this in the Inspector!

    public void InitializeLayout()
    {
        layout = new TileType[width * height];
    }

    public void ParseLayout()
    {
        string cleaned = layoutString.Replace(" ", "")
                                  .Replace("\n", "")
                                  .Replace("\r", "")
                                  .Replace("\t", "");
        layout = new TileType[width * height];
        for (int i = 0; i < cleaned.Length && i < cleaned.Length; i++)
        {
            char c = cleaned[i];
            layout[i] = CharToTileType(c);
        }
    }

    private TileType CharToTileType(char c)
    {
        return c switch
        {
            '0' => TileType.Empty,
            '1' => TileType.SpawnPoint,
            '2' => TileType.Wall
        };
    }
}
