using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MoveDetection))]
public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;
    private MoveDetection _moveDetection;
    void Awake()
    {
        _moveDetection = GetComponent<MoveDetection>();
    }
    void Start()
    {
        transform.position = new Vector3(_tileManager.GetSpawnPoint().x, _tileManager.GetSpawnPoint().y, -0.001f);
    }

    void Update()
    {

        // Directional movement controls
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            List<Vector3?> availabity = _moveDetection.GetPosition();
            if (Input.GetKeyDown(KeyCode.W) && availabity[0].HasValue)
            {
                Debug.Log("Up");
                transform.position = availabity[0].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.D) && availabity[1].HasValue)
            {
                Debug.Log("Right");
                transform.position = availabity[1].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.S) &&  availabity[2].HasValue)
            {
                Debug.Log("Down");
                transform.position = availabity[2].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.A) && availabity[3].HasValue)
            {
                Debug.Log("Left");
                transform.position = availabity[3].Value;
                PlayerEvent.PlayerMoved();
            }
        }
      
       

    
    }
}
