using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetector : MonoBehaviour
{
    [SerializeField] private int _range = 5;
    [SerializeField] private GameObject _dangerTilePrefab;

    private CircleCollider2D _circleCollider2D;
    private bool _isRendering = false;
    private GameObject _lastPlayersTile = null;
    private bool _isSeen = false;

    private List<GameObject> _dangerTilePool = new();
    public int initialPoolSize = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject tile = Instantiate(_dangerTilePrefab);
            tile.SetActive(false);
            _dangerTilePool.Add(tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void FixedUpdate()
    {
        List<RaycastHit2D> tileList = new();
        HashSet<Collider2D> seenColliders = new();
        _isSeen = false;

        int rayCount = 30; // How many directions you shoot
        float angleStep = 360f / rayCount;
        float currentAngleDeg;

        for (int i = 0; i < rayCount; i++)
        {
            currentAngleDeg = angleStep * i;
            float angleRad = currentAngleDeg * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            // Wall blocking
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, _range, LayerMask.GetMask("Wall"));
            float distance = wallHit.collider != null ? Vector2.Distance(wallHit.point, transform.position) : _range;

            // Actual hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance, LayerMask.GetMask("Ground", "Player"));
            Debug.DrawRay(transform.position, direction * distance, Color.green); // Visual debug

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && seenColliders.Add(hit.collider))
                {
                    tileList.Add(hit);

                    if (hit.collider.CompareTag("Player"))
                    {
                        _isSeen = true;
                        _lastPlayersTile = GetTileBeneath(hit.collider.transform.position);
                        Debug.Log("Player detected at angle: " + currentAngleDeg);
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

    public void SetRange(int range)
    {
        this._range = range;
    }
}
