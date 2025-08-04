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
    public int fov;
    public EnemyTile(int direction, int range, int fov, string route) : base(direction)
    {
        this.route = route;
        this.range = range;
        this.fov = fov;
    }
}

public class SoundTile : TileData
{
    public int range;
    public SoundTile(int direction, int range) : base(direction)
    {
        this.range = range;
    }
}

public class ItemTile : TileData
{
    public ItemTile(int direction) : base(direction) { }
}

public class GoalTile : TileData
{
    public GoalTile(int direction) : base(direction) { }
}
