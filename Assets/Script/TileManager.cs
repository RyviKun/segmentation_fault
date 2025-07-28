using System;
using UnityEngine;
using static UnityEngine.Rendering.STP;


public class TileManager : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _wallPrefab;
    [SerializeField] private GameObject _spawnPointPrefab;
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
                Debug.Log("Variable " + _gridConfig.layout[0].GetType());
                if (_gridConfig.layout[count].GetType() == typeof(WallTile))
                {
                    Instantiate(_wallPrefab, new Vector2(x, y), Quaternion.identity);
                }

                if (_gridConfig.layout[count].GetType() == typeof(SpawnTile))
                {
                    var spawnPointObj = Instantiate(_spawnPointPrefab, new Vector2(x, y), Quaternion.identity);
                    spawnPoint = spawnPointObj.transform.position;
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


}
