using System;
using UnityEngine;


public class TileManager : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _wallPrefab;
    [SerializeField] private GameObject _spawnPointPrefab;
    private Vector2 spawnPoint;
    void Start()
    {
        
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

                if (_gridConfig.layout[count] == TileType.Wall)
                {
                    Instantiate(_wallPrefab, new Vector2(x, y), Quaternion.identity);
                }

                if (_gridConfig.layout[count] == TileType.SpawnPoint)
                {
                    var spawnPointObj = Instantiate(_spawnPointPrefab, new Vector2(x, y), Quaternion.identity);
                    spawnPoint = spawnPointObj.transform.position;
                }

                count++;
            }
            isBlocked = !isBlocked;
        }
    }

    public Vector2 GetSpawnPoint()
    {
        return spawnPoint;
    }


}
