
using System.Collections.Generic;
using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    public LayerMask raycastMask;
    RaycastHit hitUp, hitRight, hitDown, hitLeft;

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 3f), Vector3.back * 4, Color.white);
        Debug.DrawRay(transform.position + new Vector3(1, 0, 3f), Vector3.back * 4, Color.white);
        Debug.DrawRay(transform.position + new Vector3(0, -1, 3f), Vector3.back * 4, Color.white);
        Debug.DrawRay(transform.position + new Vector3(-1, 0, 3f), Vector3.back * 4, Color.white);
    }
    public List<Vector3?> GetPosition()
    {
        Debug.Log("Hit initiated");

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector2.down, 3f, raycastMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.down, 3f, raycastMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector2.down, 3f, raycastMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + new Vector3(0, -1, 0), Vector2.down, 3f, raycastMask);

        List<Vector3?> hitPositions = new List<Vector3?>();

        void TryAddHit(RaycastHit2D hit, string direction)
        {
            if (hit.collider != null && hit.collider.CompareTag("FreeTile"))
            {
                Debug.Log($"from [{this.name}] [{direction}] Object name [{hit.transform.name}] with Tag [{hit.transform.tag}]");
                hitPositions.Add(hit.transform.position);
            }
            else
            {
                hitPositions.Add(null);
            }
        }

        // Process each direction
        TryAddHit(hitUp, "Right");
        TryAddHit(hitRight, "Up");
        TryAddHit(hitDown, "Left");
        TryAddHit(hitLeft, "Down");

        //Debug.Log(hitPositions);
        return hitPositions;
    }
    
}
