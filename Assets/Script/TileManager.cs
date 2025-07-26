using UnityEngine;


public class TileManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    void Start()
    {
        Debug.Log("Hello world!");
        bool isBlocked = true; 
        for (int y = 0; y < _height; y++)
        {
            bool test = isBlocked;
            for (int x = 0; x < _width; x++)
            {
                test = !test;
                var spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
                spawnedTile.GetComponent<SpriteRenderer>().color = test ?Color.white : Color.gray;
                
            }
            isBlocked = !isBlocked;
        }
    }


}
