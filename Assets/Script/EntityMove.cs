using UnityEngine;

[RequireComponent (typeof(MoveDetection))]
public class EntityMove : MonoBehaviour
{
    private MoveDetection _moveDetection;

    void Awake()
    {
        PlayerEvent.OnPlayerMoved += Listener;
    }
    void Start()
    {
        _moveDetection = GetComponent<MoveDetection>();
    }

    void Listener()
    {
        this.transform.position = _moveDetection.GetPosition()[0].Value;
    }
    
}
