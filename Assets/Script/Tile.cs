using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject _tileRight, _tileUp, _tileLeft, _tileDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject[] GetAllDirection()
    {
        return new GameObject[] {_tileRight, _tileUp, _tileLeft, _tileDown }.Where(tile => tile != null).ToArray();
    }
        

}
