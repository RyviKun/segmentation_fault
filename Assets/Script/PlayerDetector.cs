using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEditor.Rendering.CameraUI;
[RequireComponent (typeof(CircleCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private int _range = 5;
    [SerializeField] private int _fov = 1;
    [SerializeField] private GameObject _dangerTilePrefab;
    private CircleCollider2D _circleCollider2D;
    public int initialPoolSize = 20;
    bool _isRendering = false;


    private List<GameObject> _dangerTilePool = new ();
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
        List<RaycastHit2D> tileList = new ();
        HashSet<Collider2D> seenColliders = new();
        for (int i = _fov * 2 + 1; 0 < i; i--)
        {

            float ratio = (float)(i - _fov - 1) / (float)_range;
            float angle2 = Mathf.Atan(ratio);
            //Debug.Log("ratio : " + fov + "/" + range + " = " + ratio);
            //Debug.Log("tan inverse : " + angle2);
            float currentAngle = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 rotatedDirection2 = new Vector2(Mathf.Cos(angle2 + currentAngle), Mathf.Sin(angle2 + currentAngle));
            Debug.DrawRay(transform.position, rotatedDirection2 * _range, Color.red);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rotatedDirection2, _range, LayerMask.GetMask("Ground"));

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && seenColliders.Add(hit.collider))
                {
                    //Debug.Log("Found object : " + hit.collider.name);
                    if (!tileList.Contains(hit))
                    {
                        tileList.Add(hit);
                    }
                }
            }

        }
        //Debug.Log("List contents : " + tileList.Count);
        //Debug.Log("List contents:");
        if(!_isRendering) StartCoroutine(RenderDangerTile(tileList));


    }

  IEnumerator RenderDangerTile(List<RaycastHit2D> tileList)
    {
        _isRendering = true;
        List<GameObject> activatedTiles = new();
        for (int j = 0; j < tileList.Count; j++)
        {
            RaycastHit2D curTileList = tileList[j];
            //Debug.Log($"[{j}] = {curTileList.collider.name}");
            GameObject tile = GetDangerTileFromPool();
            tile.transform.position = curTileList.transform.position;
            tile.SetActive(true);
            activatedTiles.Add(tile);
            //Instantiate(_dangerTile, tileList[j].collider.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.2f);
        for(int i = activatedTiles.Count; i > 0; i--)
            activatedTiles[i - 1].SetActive(false);

        _isRendering = false;
    }

    GameObject GetDangerTileFromPool()
    {
        foreach (GameObject tile in _dangerTilePool)
        {
            if (!tile.activeInHierarchy)
            {
                return tile; // Found one not in use
            }
        }

        // None available, make a new one (optional)
        GameObject newTile = Instantiate(_dangerTilePrefab);
        newTile.SetActive(false);
        _dangerTilePool.Add(newTile);
        return newTile;
    }


}
