using System;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.STP;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Tile _wallPrefab;
    [SerializeField] private GameObject _spawnPointPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _itemPrefab;
    private Vector2 spawnPoint;
    void Awake()
    {
        PlayerEvent.OnPlayerMoved += PrintOut;
        bool isBlocked = true; 
        int count = 0;
        _gridConfig.ParseLayout();
        TileData[] layout = _gridConfig.layout;
        List<GameObject> spawnedTileList = new();
        LayerMask raycastMask = LayerMask.GetMask("Ground");

        for (int y = _gridConfig.height ; y > 0  ; y--)
        {
            bool test = isBlocked;
            for (int x = 0; x < _gridConfig.width; x++)
            {
                test = !test;
                GameObject spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, 1), Quaternion.identity);
                Tile spawnedTile_tileScript = spawnedTile.GetComponent<Tile>();
                spawnedTile.GetComponent<SpriteRenderer>().color = test ?Color.white : Color.gray;
                spawnedTile.name = "Tile (" + x + "," + y + ")"; 
                
                TileData current = layout[count];
                spawnedTileList.Add(spawnedTile);

                if (current is WallTile)
                {
                    Instantiate(_wallPrefab, new Vector2(x, y), Quaternion.identity);
                    spawnedTile_tileScript.isOccupied = true;
                    
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
                    enemyObj.GetComponent<EntityMove>().SetEnemyRoute(enemyTile.route);
                }

                if (current is ItemTile)
                {
                    Instantiate(_itemPrefab, new Vector2(x, y), Quaternion.identity);
                }
                count++;
                
            }
            isBlocked = !isBlocked;
        }

        foreach (GameObject spawnedTile in spawnedTileList)
        {
            Vector3 currentTilePosition = spawnedTile.transform.position;
            Tile tileComponent = spawnedTile.GetComponent<Tile>();
            RaycastHit2D hitRight = Physics2D.Raycast(currentTilePosition + new Vector3(1, 0, 0), Vector3.back, 3f, raycastMask);
            RaycastHit2D hitUp = Physics2D.Raycast(currentTilePosition + new Vector3(0, 1, 0), Vector3.back, 3f, raycastMask);
            RaycastHit2D hitLeft = Physics2D.Raycast(currentTilePosition + new Vector3(-1, 0, 0), Vector3.back, 3f, raycastMask);
            RaycastHit2D hitDown = Physics2D.Raycast(currentTilePosition + new Vector3(0, -1, 0), Vector3.back, 3f, raycastMask);
            

            if (hitRight.collider != null)
            {
                tileComponent._tileRight = hitRight.collider.gameObject;
            }
            if (hitUp.collider != null)
            {
                tileComponent._tileUp = hitUp.collider.gameObject;
            }
            if (hitLeft.collider != null)
            {
                tileComponent._tileLeft = hitLeft.collider.gameObject;
            }
            if (hitDown.collider != null)
            {
                tileComponent._tileDown = hitDown.collider.gameObject;
            }

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
