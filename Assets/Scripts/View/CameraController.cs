using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Public Variables

    // How quickly the camera moves
    public float panSpeed = 20f;

    //How far can camera pan
    public Vector2 panLimit;

    // How quickly the camera rotates
    public float rotSpeed = 10f;

    // How quickly the camera zooms
    public float zoomSpeed = 50f;

    // How quickly the camera drags
    public float dragSpeed = 20f;

    // The minimum distance of the mouse cursor from the screen edge required to pan the camera
    public float borderWidth = 10f;

    public void Initialize(LevelData levelData)
    {
        panLimit = new Vector2(levelData.Width, levelData.Height);
    }

    // Boolean to control if moving the mouse within the borderWidth distance will pan the camera
    public bool edgeScrolling = true;

    // A placeholder for a reference to the camera in the scene
    public Camera cam;

    // Private Variables
    // Minimum distance from the camera to the camera target
    private float zoomMin = 10.0f;

    // Maximum distance from the camera to the camera target
    private float zoomMax = 25.0f;

    // Floats to hold reference to the mouse position, no values to be assigned yet
    private float mouseX, mouseY;

    private readonly GameWorld gameWorld;
    private LevelData levelData => gameWorld.LevelData;

    public void Movement()
    {
        // Local variable to hold the camera target's position during each frame
        Vector3 pos = transform.position;

        // Local variable to reference the direction the camera is facing (Which is driven by the Camera target's rotation)
        Vector3 up = transform.up;

        // Ensure the camera target doesn't move up and down
        up.z = 0;

        // Normalize the X, Y & Z properties of the forward vector to ensure they are between 0 & 1
        up.Normalize();


        // Local variable to reference the direction the camera is facing + 90 clockwise degrees (Which is driven by the Camera target's rotation)
        Vector3 right = transform.right;

        // Ensure the camera target doesn't move up and down
        right.z = 0;

        // Normalize the X, Y & Z properties of the right vector to ensure they are between 0 & 1
        right.Normalize();

        // Move the camera (camera_target) Forward relative to current rotation if "W" is pressed or if the mouse moves within the borderWidth distance from the top edge of the screen
        if (!Input.GetMouseButton(2) && (Input.GetKey("w") || edgeScrolling == true && Input.mousePosition.y >= Screen.height - borderWidth))
        {
            pos += up * panSpeed * Time.deltaTime;
        }

        // Move the camera (camera_target) Backward relative to current rotation if "S" is pressed or if the mouse moves within the borderWidth distance from the bottom edge of the screen
        if (!Input.GetMouseButton(2) && (Input.GetKey("s") || edgeScrolling == true && Input.mousePosition.y <= borderWidth))
        {
            pos -= up * panSpeed * Time.deltaTime;
        }

        // Move the camera (camera_target) Right relative to current rotation if "D" is pressed or if the mouse moves within the borderWidth distance from the right edge of the screen
        if (!Input.GetMouseButton(2) && (Input.GetKey("d") || edgeScrolling == true && Input.mousePosition.x >= Screen.width - borderWidth))
        {
            pos += right * panSpeed * Time.deltaTime;
        }


        // Move the camera (camera_target) Left relative to current rotation if "A" is pressed or if the mouse moves within the borderWidth distance from the left edge of the screen
        if (!Input.GetMouseButton(2) && (Input.GetKey("a") || edgeScrolling == true && Input.mousePosition.x <= borderWidth))
        {
            pos -= right * panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x + Screen.width/2);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y - Screen.height/2, 0);

        // Setting the camera target's position to the modified pos variable
        transform.position = pos;
    }

    public void Zoom()
    {
        // Local variable to temporarily store our camera's position
        Vector3 camPos = cam.transform.position;

        // Local variable to store the distance of the camera from the camera_target
        float distance = Vector3.Distance(transform.position, cam.transform.position);

        // When we scroll our mouse wheel up, zoom in if the camera is not within the minimum distance (set by our zoomMin variable)
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && distance > zoomMin)
        {
            camPos += cam.transform.forward * zoomSpeed * Time.deltaTime;
        }

        // When we scroll our mouse wheel down, zoom out if the camera is not outside of the maximum distance (set by our zoomMax variable)
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && distance < zoomMax)
        {
            camPos -= cam.transform.forward * zoomSpeed * Time.deltaTime;
        }

        // Set the camera's position to the position of the temporary variable
        cam.transform.position = camPos;
    }


    public void Drag()
    {
        Vector3 pos = transform.position;

        if (Input.GetMouseButton(2))
        {
            pos -= new Vector3(Input.GetAxis("Mouse X") * dragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * dragSpeed * Time.deltaTime, 0);
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

    public void CameraUpdate() 
    {
        Movement();
        Drag();
        Zoom();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
