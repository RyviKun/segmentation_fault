using UnityEngine;
[RequireComponent (typeof(CircleCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    private int range;
    private CircleCollider2D _circleCollider2D;
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
        }
    }
    

}
