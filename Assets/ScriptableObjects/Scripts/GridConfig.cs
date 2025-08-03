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
                
                    break;
                case "1":
                    layout[i] = new SpawnTile(0);
      
                    break;
                case "2":
                    layout[i] = new WallTile(0);
         
                    break;
                case "3":
                    layout[i] = new EnemyTile(int.Parse(current[1]), int.Parse(current[2]), current[3]);
           
                    break;
                case "4":
                    layout[i] = new ItemTile(0);

                    break;
            }
        }
    }

   
}
