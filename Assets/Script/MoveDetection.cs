
using System.Collections.Generic;
using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    public LayerMask raycastMask;
    [SerializeField] private ItemData _itemData;

    private void Start()
    {
        _itemData.SetItemInitial();
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0, 3f), Vector3.back * 4, Color.white);
        GetItemCheck();
    }
    public GameObject GetTile()
    {
        //Debug.Log("Hit initiated");

        RaycastHit2D hitCurrentTile = Physics2D.Raycast(transform.position, Vector3.back, 3f, raycastMask);

        return hitCurrentTile.collider.gameObject;
    }

    public void GetItemCheck()
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position + new Vector3(0, 0, 0), Vector2.down, 3f);
        if (hitCenter.collider.CompareTag("Item"))
        {
            Destroy(hitCenter.collider.gameObject);
            _itemData.ItemObtained();
            Debug.Log("Obtained Item");
        }
    }
}
