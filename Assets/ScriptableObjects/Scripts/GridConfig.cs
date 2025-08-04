using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/GridConfig")]


public class GridConfig : ScriptableObject

{
    public Dictionary<int, LevelData> levels = new Dictionary<int, LevelData>()
    {
        { 1, new LevelData{
            stringLayout =
            @"1, 2, 3-3-5-3-3333333333333333311111111111110002221111, 0, 2, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 0, 0,
            0, 2, 0, 0, 0, 0,
            0, 2, 0, 0, 0, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 2, 5,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 2, 0,
            0, 2, 0, 0, 0, 0,
            0, 2, 0, 0, 0, 0,
            0, 2, 0, 0, 0, 0,
            0, 0, 0, 0, 2, 0,
            0, 0, 0, 0, 2, 0,
            0, 0, 0, 0, 2, 0,",
            width = 6,
            height = 18,
            }
        },
        { 2, new LevelData{
            stringLayout =
            @"3-3-5-3-33333331111111, 0, 2, 1, 2, 3-3-5-3-3333333333311111111111, 5,
            0, 0, 2, 0, 2, 0, 0,
            0, 0, 2, 0, 2, 0, 0,
            0, 0, 2, 0, 2, 0, 2,
            0, 0, 0, 0, 2, 0, 2,
            0, 0, 0, 0, 2, 0, 2,
            0, 0, 0, 0, 2, 0, 2,
            0, 0, 2, 2, 2, 0, 0,
            2, 0, 0, 2, 0, 0, 0,
            2, 0, 0, 2, 0, 0, 0,
            2, 0, 0, 2, 0, 0, 0,
            0, 0, 2, 2, 0, 0, 0,
            0, 0, 2, 0, 0, 0, 0,
            0, 0, 2, 0, 0, 2, 0,
            0, 0, 0, 0, 0, 2, 0,
            0, 0, 0, 0, 0, 2, 0,
            0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0,
            3-0-5-3-000000222222, 0, 0, 0, 0, 0, 0",
            width = 7,
            height = 19
            }
        },
        { 3, new LevelData{
            stringLayout =
            @"1, 0, 3-3-5-3-3333333333333311111111111111, 2, 3-3-5-3-3333333311111111,
0, 0, 0, 2, 0,
0, 0, 0, 2, 0,
2, 2, 0, 2, 0,
2, 2, 0, 2, 0,
0, 0, 0, 0, 0,
0, 0, 0, 0, 0,
3, 0, 0, 0, 0,
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
