using UnityEngine;


public class TileManager : MonoBehaviour
{
    [SerializeField] private GridConfig _gridConfig;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _wallPrefab;
    void Start()
    {
        
        bool isBlocked = true; 
        int count = 0;
        for (int y = 0; y < _gridConfig.height; y++)
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
                
                count++;
            }
            isBlocked = !isBlocked;
        }
    }



}
