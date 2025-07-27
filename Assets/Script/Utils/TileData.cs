using UnityEngine;
[System.Serializable]
public struct TileData
{
    public TileType type;
    public int direction;

    public TileData(TileType type, int direction = 0)
    {
        this.type = type;
        this.direction = direction;
    }
}
