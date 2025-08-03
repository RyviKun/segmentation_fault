
using System.Collections.Generic;
using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    public LayerMask raycastMask;
    

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0, 3f), Vector3.back * 4, Color.white);

    }
    public GameObject GetTile()
    {
        //Debug.Log("Hit initiated");

        RaycastHit2D hitCurrentTile = Physics2D.Raycast(transform.position, Vector3.back, 3f, raycastMask);

        return hitCurrentTile.collider.gameObject;
    }
    
}
