using UnityEngine;

[System.Serializable]
public abstract class TileData
{
    public int direction;

    public TileData(int direction)
    {
        this.direction = direction;
    }
}

public class EmptyTile : TileData
{
    public EmptyTile(int direction) : base(direction) { }
}

public class WallTile : TileData
{
    public WallTile(int direction) : base(direction) { }
}

public class SpawnTile : TileData
{
    public SpawnTile(int direction) : base(direction) { }
}

public class EnemyTile : TileData
{
    public string route;
    public int range;
    public EnemyTile(int direction, int range, string route) : base(direction)
    {
        this.route = route;
        this.range = range;
    }
}

public class ItemTile : TileData
{
    public ItemTile(int direction) : base(direction) { }
}
