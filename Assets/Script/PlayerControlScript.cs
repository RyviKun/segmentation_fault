using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MoveDetection))]
public class PlayerControlScript : MonoBehaviour
{
    [SerializeField] private TileManager _tileManager;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private WinLose _winLose;
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
            Tile availability = _moveDetection.GetTile().GetComponent<Tile>();
            
            if (Input.GetKeyDown(KeyCode.D) && availability._tileRight && !availability._tileRight.gameObject.GetComponent<Tile>().isOccupied)
            {
                if (_itemData.GetItemStatus() && _itemData.GetEffectTimer() > 0)
                {
                    _itemData.CountDownTimer();
                }
                else
                {
                    _itemData.ItemEffectOff();
                }
                transform.position = availability._tileRight.transform.position;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.W) && availability._tileUp && !availability._tileUp.gameObject.GetComponent<Tile>().isOccupied)
            {
                if (_itemData.GetItemStatus() && _itemData.GetEffectTimer() > 0)
                {
                    _itemData.CountDownTimer();
                }
                else
                {
                    _itemData.ItemEffectOff();
                }
                transform.position = availability._tileUp.transform.position;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.A) && availability._tileLeft && !availability._tileLeft.gameObject.GetComponent<Tile>().isOccupied)
            {
                if (_itemData.GetItemStatus() && _itemData.GetEffectTimer() > 0)
                {
                    _itemData.CountDownTimer();
                }
                else
                {
                    _itemData.ItemEffectOff();
                }
                transform.position = availability._tileLeft.transform.position;
                PlayerEvent.PlayerMoved();
            }
            else if (Input.GetKeyDown(KeyCode.S) && availability._tileDown && !availability._tileDown.gameObject.GetComponent<Tile>().isOccupied)
            {
                if (_itemData.GetItemStatus() && _itemData.GetEffectTimer() > 0)
                {
                    _itemData.CountDownTimer();
                }
                else
                {
                    _itemData.ItemEffectOff();
                }
                transform.position = availability._tileDown.transform.position;
                PlayerEvent.PlayerMoved();
            }
            GameObject currentTile = _moveDetection.GetTile();
            Debug.Log("current Tile : " + currentTile.name);
            if (currentTile != null && currentTile.CompareTag("GoalTile"))
            {
                Debug.Log("Win!");
                _winLose.GameWin();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_itemData.GetItemAvailability())
            {
                _itemData.ItemUsed();
            }
        }
    }
}
