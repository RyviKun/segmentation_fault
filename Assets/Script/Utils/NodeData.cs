using UnityEngine;

public class NodeData
{
    public GameObject tile;
    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;
    public NodeData parent;
}
