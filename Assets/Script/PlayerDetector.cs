using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private int _range = 5;
    [SerializeField] private int _fov = 1;
    [SerializeField] private GameObject _dangerTilePrefab;

    private CircleCollider2D _circleCollider2D;
    private bool _isRendering = false;
    private GameObject _lastPlayersTile = null;
    private bool _isSeen = false;

    private List<GameObject> _dangerTilePool = new();
    public int initialPoolSize = 20;

    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject tile = Instantiate(_dangerTilePrefab);
            tile.SetActive(false);
            _dangerTilePool.Add(tile);
        }
    }

    private void FixedUpdate()
    {
        List<RaycastHit2D> tileList = new();
        HashSet<Collider2D> seenColliders = new();
        _isSeen = false; // Reset every frame

        for (int i = 0; i < _fov * 2 + 1; i++)
        {
            float ratio = (float)(i - _fov) / _range;
            float angle = Mathf.Atan(ratio);
            float currentAngle = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 rotatedDirection = new Vector2(Mathf.Cos(angle + currentAngle), Mathf.Sin(angle + currentAngle));

            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, rotatedDirection, _range, LayerMask.GetMask("Wall"));
            float distance = wallHit.collider != null ? Vector2.Distance(wallHit.point, transform.position) : _range;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rotatedDirection, distance, LayerMask.GetMask("Ground", "Player"));
            Debug.DrawRay(transform.position, rotatedDirection * distance, Color.red);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && seenColliders.Add(hit.collider))
                {
                    tileList.Add(hit);

                    if (hit.collider.CompareTag("Player"))
                    {
                        _isSeen = true;
                        _lastPlayersTile = GetTileBeneath(hit.collider.transform.position);
                        Debug.Log("Found player!");
                    }
                }
            }
        }

        if (!_isRendering)
        {
            StartCoroutine(RenderDangerTile(tileList));
        }
    }

    GameObject GetTileBeneath(Vector3 worldPosition)
    {
        // Cast ray downward to find the tile directly under the player
        RaycastHit2D hit = Physics2D.Raycast(worldPosition + Vector3.back * 0.5f, Vector3.forward, 1f, LayerMask.GetMask("Ground"));
        return hit.collider != null ? hit.collider.gameObject : null;
    }

    IEnumerator RenderDangerTile(List<RaycastHit2D> tileList)
    {
        _isRendering = true;
        List<GameObject> activatedTiles = new();

        foreach (var tileHit in tileList)
        {
            GameObject tile = GetDangerTileFromPool();
            tile.transform.position = tileHit.collider.transform.position;
            tile.SetActive(true);
            activatedTiles.Add(tile);
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var tile in activatedTiles)
        {
            tile.SetActive(false);
        }

        _isRendering = false;
    }

    GameObject GetDangerTileFromPool()
    {
        foreach (var tile in _dangerTilePool)
        {
            if (!tile.activeInHierarchy)
                return tile;
        }

        GameObject newTile = Instantiate(_dangerTilePrefab);
        newTile.SetActive(false);
        _dangerTilePool.Add(newTile);
        return newTile;
    }

    // === Accessors ===
    public GameObject? GetTile()
    {
        return _lastPlayersTile;
    }

    public void ClearPlayerTransform()
    {
        _lastPlayersTile = null;
    }

    public bool GetIsSeen()
    {
        return _isSeen;
    }
}
