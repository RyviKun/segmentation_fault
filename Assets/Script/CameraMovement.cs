using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool followingPlayer = true;
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector2 resetPosition;
    private float width;
    private float height;
    private Vector3 absolutePos;
    [Header("GridObject")]
    public GridConfig _gridConfig;

    [Header("Player Object")]
    public GameObject plr;

    // [Header("Limiters")]

    private float LeftXLimit = -10f;

    private float RightXLimit = 10f;

    private float UpperYLimit = 10f;

    private float BottomYLimit = -10f;
    void Start()
    {

        resetPosition = plr.transform.position;
        width = _gridConfig.width;
        height = _gridConfig.height;
        absolutePos = new Vector3(-.5f + (width / 2), .5f + (height / 2), -10);
        setCamLimit();
        Camera.main.transform.position = new Vector3(resetPosition.x, resetPosition.y, -10);

    }

    // Update is called once per frame
    void Update()
    {
        resetPosition = plr.transform.position;

        PanCamera();
        ResetCamera();


    }

    private void ResetCamera()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            followingPlayer = true;
            Debug.Log("Camera Reset");
            Debug.Log(resetPosition);
            Camera.main.transform.position = new Vector3(resetPosition.x, resetPosition.y, -10);
        }
        if (followingPlayer == true)
        {
            Camera.main.transform.position = new Vector3(resetPosition.x, resetPosition.y, -10);
        }
    }
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            followingPlayer = false;
            Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            followingPlayer = false;
            Difference = Origin - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            print("origin" + Origin + " newPosition " + Camera.main.ScreenToWorldPoint(Input.mousePosition) + " =difference " + Difference);
            Camera.main.transform.position = ClampCamera(Camera.main.transform.position + Difference);
        }
    }
    private Vector3 ClampCamera(Vector3 targetPos)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minX = LeftXLimit + camWidth;
        float maxX = RightXLimit - camWidth;
        float minY = BottomYLimit + camHeight;
        float maxY = UpperYLimit - camHeight;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(newX, newY, targetPos.z);
    }
    private void setCamLimit()
    {
        // LeftXLimit = (-.5f) - 10f;
        // RightXLimit = (.5f + width) + 10f;
        // BottomYLimit = .5f - 10f;
        // UpperYLimit = (1.5f + height) + 10f;
        LeftXLimit = (-.5f) - 10f;
        RightXLimit = (.5f + width - 1) + 10f;
        BottomYLimit = .5f - 6f;
        UpperYLimit = (1.5f + height - 1) + 6f;

        Debug.Log("LeftXLimit" + LeftXLimit);
        Debug.Log("RightXLimit" + RightXLimit);
        Debug.Log("BottomYLimit" + BottomYLimit);
        Debug.Log("UpperYLimit" + UpperYLimit);
        // Debug.Log("LeftXLimit" + LeftXLimit);
        // Debug.Log("RightXLimit" + RightXLimit);
        // Debug.Log("BottomYLimit" + BottomYLimit);
        // Debug.Log("BottomYLimit" + BottomYLimit);

    }

}


