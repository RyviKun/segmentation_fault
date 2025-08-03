using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MoveDetection))]
public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private ItemData _itemData;
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            List<Vector3?> availabity = _moveDetection.GetPosition();
            if (Input.GetKeyDown(KeyCode.D) && availabity[0].HasValue)
            {
                Debug.Log("Up");
                NotifyTimer();
                transform.position = availabity[0].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.W) && availabity[1].HasValue)
            {
                Debug.Log("Right");
                NotifyTimer();
                transform.position = availabity[1].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.A) &&  availabity[2].HasValue)
            {
                Debug.Log("Down");
                NotifyTimer();
                transform.position = availabity[2].Value;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.S) && availabity[3].HasValue)
            {
                Debug.Log("Left");
                NotifyTimer();
                transform.position = availabity[3].Value;
                PlayerEvent.PlayerMoved();
            }
        }
        _moveDetection.GetItemCheck();

        if (Input.GetKeyDown(KeyCode.Space) && _itemData.GetItemAvailability())
        {
            _itemData.ItemUsed();
        }
    }
    public void NotifyTimer()
    {
        if (_itemData.GetItemStatus())
        {
            if (_itemData.GetEffectTimer() > 0)
            {
                _itemData.CountDownTimer();
                Debug.Log("Item in effect!\nTimer :" + _itemData.GetEffectTimer());
            }
            else
            {
                _itemData.ItemEffectOff();
                Debug.Log("Item effect wore off!");
            }
        }
        else
        {
            Debug.Log("Item not in effect");
        }
    }
}
