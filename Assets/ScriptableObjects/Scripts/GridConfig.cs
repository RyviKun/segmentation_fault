using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]


public class GridConfig : ScriptableObject

{
    public Dictionary<int, LevelData> levels = new Dictionary<int, LevelData>()
    {
        { 1, new LevelData{
            stringLayout =
            @"0, 1, 0, 0,
            0, 0, 0, 4-2, 0, 
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 3-1-3-000111, 
            0, 0, 0, 0",
            width = 10,
            height = 10,
            }
        },
        { 2, new LevelData{
            stringLayout =
            @"0, 0, 0, 0, 0,
            0, 1, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0",
            width = 5,
            height = 5
            }
        },
        { 3, new LevelData{
            stringLayout =
            @"0, 0, 0, 0, 0,
            0, 1, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            0, 0, 0, 0, 0",
=======
=======
>>>>>>> Stashed changes
            0, 0, 0, 0, 0,
            0, 2, 0, 0, 0,
            0, 2, 0, 0, 2,
            0, 2, 0, 0, 2,
            0, 0, 0, 0, 2,
            0, 0, 0, 0, 2,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 2, 2, 2, 0,
            0, 2, 2, 2, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            3-1-5-3-333333333333333111111111111111, 0, 0, 0, 5,",
>>>>>>> Stashed changes
            width = 5,
            height = 20
            }
        }
    };
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
        for (int i = 0; i < splitted.Length; i++)
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
                    layout[i] = new SoundTile(int.Parse(current[1]), int.Parse(current[1]));

                    break;
            }
        }
    }


}
