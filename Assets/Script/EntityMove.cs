using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent (typeof(MoveDetection))]
public class EntityMove : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    private MoveDetection _moveDetection;
    private List<int> _route = new();
    private int _routeIndex = 0;
    private bool _isChasing = false;
    private PlayerDetector _playerDetector;
    void Awake()
    {
        PlayerEvent.OnPlayerMoved += Listener;
    }
    void Start()
    {
        _moveDetection = GetComponent<MoveDetection>();
        _playerDetector = GetComponent<PlayerDetector>(); 
    }

    private void Update()
    {
        //Debug.Log(_playerDetector.GetPlayerPosition());
    }

    void Listener()
    {
        if (_playerDetector.GetPlayerTransform() != null) _isChasing = true;
        if (!_isChasing)
        {
            Debug.Log("route Index : " + _routeIndex);
            Debug.Log("current Route " + _route[_routeIndex]);
            int moveDirection = _route[_routeIndex];
            this.transform.position = _moveDetection.GetPosition()[moveDirection].Value;
            this.transform.rotation = ParseRotation(moveDirection);
            _routeIndex++;
        }
        else if (_isChasing && !_itemData.GetItemStatus())
        {
            Debug.Log("chasing");
            Transform? currentPlayerPosition = _playerDetector.GetPlayerTransform();
            Vector2 finalPosition = transform.position;
            Vector2[] directions = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };
            float shortestDistance = float.MaxValue;

            foreach (Vector2 dir in directions)
            {
                Vector2 candidatePosition = (Vector2)transform.position + dir;
                float distance = Vector2.Distance(candidatePosition, currentPlayerPosition.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    finalPosition = candidatePosition;
                }
            }
            transform.position = finalPosition;
            Vector2 direction = currentPlayerPosition.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    
    public void SetEnemyRoute(string route)
    {
        
        for (int i = 0; i < route.Length; i++)
        {
            
            _route.Add((int)char.GetNumericValue(route[i]));
           
        }
  
    }

    public static Quaternion ParseRotation(int direction)
    {
        switch (direction)
        {
            case 0: return Quaternion.Euler(0, 0, 0);      // Right
            case 1: return Quaternion.Euler(0, 0, 90);    // Up
            case 2: return Quaternion.Euler(0, 0, 180);    // Left
            case 3: return Quaternion.Euler(0, 0, 270);     // Down
            default:
                Debug.LogWarning("Invalid direction: " + direction);
                return Quaternion.identity;
        }
    }
} 
