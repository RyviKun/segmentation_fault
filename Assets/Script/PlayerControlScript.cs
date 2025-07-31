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
            if (Input.GetKeyDown(KeyCode.D) && availabity[0].HasValue)
            {
                transform.position = availabity[0].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.W) && availabity[1].HasValue)
            {
           
                transform.position = availabity[1].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.A) &&  availabity[2].HasValue)
            {
                transform.position = availabity[2].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.S) && availabity[3].HasValue)
            {
                transform.position = availabity[3].Value;
                PlayerEvent.PlayerMoved();
            }
        }
      
       

    
    }
}
