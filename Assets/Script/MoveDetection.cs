using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    private Vector2 tilePosition;
    private bool isMovableTo = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FreeTile")
        {
            isMovableTo = true;
            tilePosition = collision.transform.position;
        }
        else
        {
            isMovableTo = false;
            tilePosition = collision.transform.position;
        }
    }

    public Vector2 GetTilePosition()
    {
        return tilePosition;
    }

    public bool IsMovableTo()
    {
        return isMovableTo;
    }
}
