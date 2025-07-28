using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]
public class GridConfig : ScriptableObject
{
    public int width, height;
    [SerializeField] public TileData[] layout;
    [TextArea]
    public string layoutString;

    public void InitializeLayout()
    {
        layout = new TileData[width * height];
    }

    public void ParseLayout()
    {
        InitializeLayout();
        string cleaned = layoutString.Replace(" ", "")
                                  .Replace("\n", "")
                                  .Replace("\r", "")
                                  .Replace("\t", "");
        string[] splitted = cleaned.Split(',');
        for (int i = 0; i < splitted.Length ; i++)
        {
            string[] current = splitted[i].Split("-");
            switch (current[0])
            {
                case "0":
                    layout[i] = new EmptyTile(0);
                    Debug.Log("Empty");
                    break;
                case "1":
                    layout[i] = new SpawnTile(0);
                    Debug.Log("Player");
                    break;
                case "2":
                    layout[i] = new WallTile(0);
                    Debug.Log("Wall");
                    break;
                case "3":
                    layout[i] = new EnemyTile(0, current[1], int.Parse(current[2]));
                    Debug.Log("Enemy");
                    break;

            }
        }
    }

   
}
