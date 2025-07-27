using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;

    [Header("Movement Colliders")]
    [SerializeField] private MoveDetection leftTile;
    [SerializeField] private MoveDetection rightTile;
    [SerializeField] private MoveDetection upTile;
    [SerializeField] private MoveDetection downTile;
    void Start()
    {
        transform.position = new Vector3(_tileManager.GetSpawnPoint().x, _tileManager.GetSpawnPoint().y, -0.001f);
    }

    void Update()
    {

        // Directional movement controls
        if (Input.GetKeyDown(KeyCode.A) && leftTile.IsMovableTo())
        {
            transform.position = Vector2.MoveTowards(this.transform.position, leftTile.GetTilePosition(), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.D) && rightTile.IsMovableTo())
        {
            transform.position = Vector2.MoveTowards(this.transform.position, rightTile.GetTilePosition(), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.W) && upTile.IsMovableTo())
        {
            transform.position = Vector2.MoveTowards(this.transform.position, upTile.GetTilePosition(), 1f);
        }
        else if (Input.GetKeyDown(KeyCode.S) && downTile.IsMovableTo())
        {
            transform.position = Vector2.MoveTowards(this.transform.position, downTile.GetTilePosition(), 1f);
        }
        else
        {

        }
    }
}
