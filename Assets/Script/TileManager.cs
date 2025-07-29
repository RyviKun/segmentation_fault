using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.STP;


public class TileManager : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _wallPrefab;
    [SerializeField] private GameObject _spawnPointPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    private Vector2 spawnPoint;
    void Awake()
    {
        PlayerEvent.OnPlayerMoved += PrintOut;
        bool isBlocked = true; 
        int count = 0;
        for (int y = _gridConfig.height ; y > 0  ; y--)
        {
            bool test = isBlocked;
            for (int x = 0; x < _gridConfig.width; x++)
            {
                test = !test;
                var spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
                spawnedTile.GetComponent<SpriteRenderer>().color = test ?Color.white : Color.gray;
                _gridConfig.ParseLayout();
                TileData current = _gridConfig.layout[count];
                if (current is WallTile)
                {
                    Instantiate(_wallPrefab, new Vector2(x, y), Quaternion.identity);
                }

                if (current is SpawnTile)
                {
                    var spawnPointObj = Instantiate(_spawnPointPrefab, new Vector2(x, y), Quaternion.identity);
                    spawnPoint = spawnPointObj.transform.position;
                }

                if (current is EnemyTile enemyTile)
                {
                    var enemyObj = Instantiate(_enemyPrefab, new Vector2(x, y), ParseRotation(enemyTile.direction));
                    Debug.Log("Rotation " + enemyTile.direction);
                    enemyObj.GetComponent<CircleCollider2D>().radius = enemyTile.range;
                }
                count++;
            }
            isBlocked = !isBlocked;
        }
    }

    public void PrintOut()
    {
        Debug.Log("Player has moved!");
    }
    public Vector2 GetSpawnPoint()
    {
        return spawnPoint;
    }

    public static Quaternion ParseRotation(int direction)
    {
        switch (direction)
        {
            case 0: return Quaternion.Euler(0, 0, 0);      // Up
            case 1: return Quaternion.Euler(0, 0, -90);    // Right
            case 2: return Quaternion.Euler(0, 0, 180);    // Down
            case 3: return Quaternion.Euler(0, 0, 90);     // Left
            default:
                Debug.LogWarning("Invalid direction: " + direction);
                return Quaternion.identity;
        }
    }

}
