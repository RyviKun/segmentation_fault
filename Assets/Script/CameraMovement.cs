using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 Origin;
    private Vector3 Difference;
    private Vector2 resetPosition;

    [Header("Player Object")]
    public GameObject plr;

    [Header("Limiters")]

    public float LeftXLimit = -10f;

    public int RightXLimit = 10;

    public int UpperYLimit = 10;

    public int BottomYLimit = -10;
    void Start()
    {
        resetPosition = plr.transform.position;
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
            Debug.Log("Camera Reset");
            Debug.Log(resetPosition);
            Camera.main.transform.position = new Vector3(resetPosition.x, resetPosition.y, -10);
        }
    }
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
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

}


