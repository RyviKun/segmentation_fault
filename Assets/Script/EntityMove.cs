using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MoveDetection))]
public class EntityMove : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    private MoveDetection _moveDetection;
    private PlayerDetector _playerDetector;

    private List<int> _route = new();
    private int _routeIndex = 0;
    private Transform _lastTile = null;
    private bool _isChasing = false;

    void Awake()
    {
        PlayerEvent.OnPlayerMoved += Listener;
    }

    void Start()
    {
        _moveDetection = GetComponent<MoveDetection>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    void Listener()
    {
        GameObject startTile = _moveDetection.GetTile();
        GameObject goalTile = _playerDetector.GetTile();

        if (_playerDetector.GetIsSeen())
        {
            _isChasing = true;
        }

        // === CHASING MODE ===
        if (_isChasing)
        {
            if (goalTile == null)
            {
                if (_lastTile == null) return;
                goalTile = _lastTile.gameObject;
            }
            else
            {
                _lastTile = goalTile.transform;
            }

            Dictionary<GameObject, NodeData> open = new();
            Dictionary<GameObject, NodeData> closed = new();

            NodeData startNode = new()
            {
                tile = startTile,
                gCost = 0f,
                hCost = Vector2.Distance(startTile.transform.position, goalTile.transform.position),
                parent = null
            };
            open[startTile] = startNode;

            while (open.Count > 0)
            {
                var current = open.OrderBy(n => n.Value.fCost).First();

                if (current.Key == goalTile)
                {
                    closed[current.Key] = current.Value;
                    break;
                }

                open.Remove(current.Key);
                closed[current.Key] = current.Value;

                GameObject[] neighbors = current.Key.GetComponent<Tile>().GetAllDirection();
                foreach (GameObject neighbor in neighbors)
                {
                    if (neighbor == null || closed.ContainsKey(neighbor)) continue;

                    Tile neighborTile = neighbor.GetComponent<Tile>();
                    if (neighborTile != null && neighborTile.isOccupied) continue;

                    float tentativeG = current.Value.gCost + 1;

                    if (!open.ContainsKey(neighbor) || tentativeG < open[neighbor].gCost)
                    {
                        NodeData newNode = new()
                        {
                            tile = neighbor,
                            gCost = tentativeG,
                            hCost = Vector2.Distance(neighbor.transform.position, goalTile.transform.position),
                            parent = current.Value
                        };
                        open[neighbor] = newNode;
                    }
                }
            }

            if (!closed.ContainsKey(goalTile))
            {
                _isChasing = false;
                return;
            }

            List<GameObject> path = new();
            NodeData node = closed[goalTile];
            while (node != null)
            {
                path.Add(node.tile);
                node = node.parent;
            }
            path.Reverse();

            if (path.Count > 1)
            {
                Vector2 toPlayerDir = _lastTile.position - transform.position;
                transform.position = path[1].transform.position;

                float angle = Mathf.Atan2(toPlayerDir.y, toPlayerDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
            else
            {
                _isChasing = false;
            }
        }

        // === PATROLLING MODE ===
        else
        {
            if (_route.Count == 0) return;

            int moveDirection = _route[_routeIndex];
            GameObject currentTile = _moveDetection.GetTile();
            Tile tile = currentTile.GetComponent<Tile>();

            GameObject targetTile = moveDirection switch
            {
                0 => tile._tileRight,
                1 => tile._tileUp,
                2 => tile._tileLeft,
                3 => tile._tileDown,
                _ => null
            };

            if (targetTile != null && !targetTile.GetComponent<Tile>().isOccupied)
            {
                Vector2 dir = targetTile.transform.position - transform.position;
                transform.position = targetTile.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            _routeIndex++;
            if (_routeIndex >= _route.Count)
            {
                _route.Reverse();
                _routeIndex = 0;
            }
        }
    }

    public void SetEnemyRoute(string route)
    {
        _route.Clear();
        for (int i = 0; i < route.Length; i++)
        {
            _route.Add((int)char.GetNumericValue(route[i]));
        }
    }

    public static Quaternion ParseRotation(int direction)
    {
        return direction switch
        {
            0 => Quaternion.Euler(0, 0, 0),     // Right
            1 => Quaternion.Euler(0, 0, 90),    // Up
            2 => Quaternion.Euler(0, 0, 180),   // Left
            3 => Quaternion.Euler(0, 0, 270),   // Down
            _ => Quaternion.identity
        };
    }

    public class NodeData
    {
        public GameObject tile;
        public float gCost;
        public float hCost;
        public float fCost => gCost + hCost;
        public NodeData parent;
    }
}
