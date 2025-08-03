using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MoveDetection))]
public class EntityMove : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] public WinLose winLose;
    private MoveDetection _moveDetection;
    private PlayerDetector _playerDetector;

    private List<int> _route = new();
    private int _routeIndex = 0;
    private Transform _lastTile = null;
    private bool _isChasing = false;
    private bool _returningToPatrol = false;


    private GameObject _interruptedPatrolTile = null;

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
        Vector3 currentTilePosition = transform.position;
        LayerMask raycastMask = LayerMask.GetMask("Player");
        RaycastHit2D hitRight = Physics2D.Raycast(currentTilePosition + new Vector3(1, 0, 0), Vector3.back, 3f, raycastMask);
        RaycastHit2D hitUp = Physics2D.Raycast(currentTilePosition + new Vector3(0, 1, 0), Vector3.back, 3f, raycastMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(currentTilePosition + new Vector3(-1, 0, 0), Vector3.back, 3f, raycastMask);
        RaycastHit2D hitDown = Physics2D.Raycast(currentTilePosition + new Vector3(0, -1, 0), Vector3.back, 3f, raycastMask);

        if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
        {
            winLose.GameOver();
        }
        if (hitUp.collider != null && hitUp.collider.CompareTag("Player"))
        {
            winLose.GameOver();
        }
        if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
        {
            winLose.GameOver();
        }
        if (hitDown.collider != null && hitDown.collider.CompareTag("Player"))
        {
            winLose.GameOver();
        }

        GameObject startTile = _moveDetection.GetTile();
        GameObject goalTile = _playerDetector.GetTile();

        // === Player Seen → Start chasing
        if (_playerDetector.GetIsSeen())
        {
            if (!_isChasing && !_returningToPatrol)
                _interruptedPatrolTile = startTile;

            _isChasing = true;
            _returningToPatrol = false;
        }

        // === A* target logic
        GameObject targetTile = null;
        if (_isChasing && !_itemData.GetItemStatus())
        {
            if (goalTile == null)
            {
                if (_lastTile == null) return;
                targetTile = _lastTile.gameObject;
            }
            else
            {
                targetTile = goalTile;
                _lastTile = goalTile.transform;
            }
        }
        else if (_returningToPatrol && _interruptedPatrolTile != null)
        {
            targetTile = _interruptedPatrolTile;
        }

        // === A* CHASING or RETURNING TO PATROL ===
        if ((targetTile != null && (_isChasing || _returningToPatrol)))
        {
            GameObject pathStartTile = _moveDetection.GetTile();

            Dictionary<GameObject, NodeData> open = new();
            Dictionary<GameObject, NodeData> closed = new();

            NodeData startNode = new()
            {
                tile = pathStartTile,
                gCost = 0f,
                hCost = Vector2.Distance(pathStartTile.transform.position, targetTile.transform.position),
                parent = null
            };
            open[pathStartTile] = startNode;

            while (open.Count > 0)
            {
                var current = open.OrderBy(n => n.Value.fCost).First();

                if (current.Key == targetTile)
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
                            hCost = Vector2.Distance(neighbor.transform.position, targetTile.transform.position),
                            parent = current.Value
                        };
                        open[neighbor] = newNode;
                    }
                }
            }

            if (!closed.ContainsKey(targetTile))
            {
                _isChasing = false;
                _returningToPatrol = true;
                return;
            }

            List<GameObject> path = new();
            NodeData node = closed[targetTile];
            while (node != null)
            {
                path.Add(node.tile);
                node = node.parent;
            }
            path.Reverse();

            if (path.Count > 1)
            {
                Vector2 toTarget = path[1].transform.position - transform.position;
                transform.position = path[1].transform.position;
                float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            // === Chase or return ends ===
            if (path.Count <= 1)
            {
                if (_isChasing && !_playerDetector.GetIsSeen())
                {
                    _isChasing = false;
                    _returningToPatrol = true;
                }
                else if (_returningToPatrol)
                {
                    _returningToPatrol = false;
                    ResumeRouteFromClosest(_moveDetection.GetTile());
                }
            }

            return;
        }

        // === PATROLLING ===
        if (_route.Count == 0) return;

        int moveDirection = _route[_routeIndex];
        GameObject currentTile = _moveDetection.GetTile();
        Tile tile = currentTile.GetComponent<Tile>();

        GameObject nextTile = moveDirection switch
        {
            0 => tile._tileRight,
            1 => tile._tileUp,
            2 => tile._tileLeft,
            3 => tile._tileDown,
            _ => null
        };

        if (nextTile != null && !nextTile.GetComponent<Tile>().isOccupied)
        {
            Vector2 dir = nextTile.transform.position - transform.position;
            transform.position = nextTile.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        _routeIndex++;
        if (_routeIndex >= _route.Count)
        {
            _routeIndex = 0;
        }
    }

    void ResumeRouteFromClosest(GameObject currentTile)
    {
        if (currentTile == null || _route.Count == 0) return;

        GameObject bestTile = null;
        float bestDist = float.MaxValue;
        int bestIndex = 0;

        for (int i = 0; i < _route.Count; i++)
        {
            GameObject patrolTile = GetTileByDirectionIndex(i, currentTile);
            if (patrolTile == null) continue;

            float dist = Vector2.Distance(currentTile.transform.position, patrolTile.transform.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                bestTile = patrolTile;
                bestIndex = i;
            }
        }

        _routeIndex = bestIndex;
    }

    GameObject GetTileByDirectionIndex(int index, GameObject currentTile)
    {
        Tile tile = currentTile.GetComponent<Tile>();
        return _route[index] switch
        {
            0 => tile._tileRight,
            1 => tile._tileUp,
            2 => tile._tileLeft,
            3 => tile._tileDown,
            _ => null
        };
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
