using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool _followingPlayer = true;
    private Vector3 _origin;
    private Vector3 _difference;
    private Vector2 _resetPosition;
    private float _width;
    private float _height;
    private Vector3 _absolutePos;
    [Header("GridObject")]
    public GridConfig _gridConfig;

    [Header("Player Object")]
    public GameObject plr;

    // [Header("Limiters")]

    private float _leftXLimit = -10f;

    private float _rightXLimit = 10f;

    private float _upperYLimit = 10f;

    private float _bottomYLimit = -10f;
    void Start()
    {

        _resetPosition = plr.transform.position;
        _width = _gridConfig.width;
        _height = _gridConfig.height;
        _absolutePos = new Vector3(-.5f + (_width / 2), .5f + (_height / 2), -10);
        setCamLimit();
        Camera.main.transform.position = new Vector3(_resetPosition.x, _resetPosition.y, -10);

    }

    // Update is called once per frame
    void Update()
    {
        _resetPosition = plr.transform.position;

        PanCamera();
        ResetCamera();


    }

    private void ResetCamera()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _followingPlayer = true;
            Debug.Log("Camera Reset");
            Debug.Log(_resetPosition);
            Camera.main.transform.position = new Vector3(_resetPosition.x, _resetPosition.y, -10);
        }
        if (_followingPlayer == true)
        {
            Camera.main.transform.position = new Vector3(_resetPosition.x, _resetPosition.y, -10);
        }
    }
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _followingPlayer = false;
            _origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            _followingPlayer = false;
            _difference = _origin - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // print("origin" + _origin + " newPosition " + Camera.main.ScreenToWorldPoint(Input.mousePosition) + " =difference " + _difference);
            Camera.main.transform.position = ClampCamera(Camera.main.transform.position + _difference);
        }
    }
    private Vector3 ClampCamera(Vector3 targetPos)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minX = _leftXLimit + camWidth;
        float maxX = _rightXLimit - camWidth;
        float minY = _bottomYLimit + camHeight;
        float maxY = _upperYLimit - camHeight;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(newX, newY, targetPos.z);
    }
    private void setCamLimit()
    {

        _leftXLimit = (-.5f) - 10f;
        _rightXLimit = (.5f + _width - 1) + 10f;
        _bottomYLimit = .5f - 6f;
        _upperYLimit = (1.5f + _height - 1) + 6f;

        // Debug.Log("LeftXLimit" + _leftXLimit);
        // Debug.Log("RightXLimit" + _rightXLimit);
        // Debug.Log("BottomYLimit" + _bottomYLimit);
        // Debug.Log("UpperYLimit" + _upperYLimit);

    }

}


