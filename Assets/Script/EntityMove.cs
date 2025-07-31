using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent (typeof(MoveDetection))]
public class EntityMove : MonoBehaviour
{
    private MoveDetection _moveDetection;
    private List<int> _route = new();
    private int _routeIndex = 0;
    void Awake()
    {
        PlayerEvent.OnPlayerMoved += Listener;
    }
    void Start()
    {
        _moveDetection = GetComponent<MoveDetection>();
    }

    void Listener()
    {
        Debug.Log("route Index : " + _routeIndex);
        Debug.Log("current Route " + _route[_routeIndex]);
        int moveDirection = _route[_routeIndex];
        this.transform.position = _moveDetection.GetPosition()[moveDirection].Value;
        this.transform.rotation = ParseRotation(moveDirection);
        _routeIndex++;
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
