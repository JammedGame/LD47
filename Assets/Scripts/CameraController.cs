using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Maximum distance from the camera to the camera target
	private const float ZoomMax = 10.0f;

	// Minimum distance from the camera to the camera target
	private const float ZoomMin = 1.0f;

	// How quickly the camera moves
	public float panSpeed = 20f;

	//How far can camera pan
	public Vector2 panLimit;

	// How quickly the camera zooms
	public float zoomSpeed = 2f;

	// A placeholder for a reference to the camera in the scene
	public Camera cam;

	private Vector3 lastMousePos;

	// Floats to hold reference to the mouse position, no values to be assigned yet
	private float mouseX, mouseY;

	private float targetZoom;

	// Start is called before the first frame update
	private void Start()
	{
		cam = Camera.main;
	}

	public void Initialize(LevelData levelData)
	{
		panLimit = new Vector2(levelData.Width, levelData.Height);
		transform.position = levelData.InitialCameraPan;
		targetZoom = cam.orthographicSize = levelData.InitialCameraZoom;
	}

	private void Movement()
	{
		// Local variable to hold the camera target's position during each frame
		var t = transform;
		var pos = t.position;

		// Local variable to reference the direction the camera is facing (Which is driven by the Camera target's rotation)
		var up = t.up;

		// Ensure the camera target doesn't move up and down
		up.z = 0;

		// Normalize the X, Y & Z properties of the forward vector to ensure they are between 0 & 1
		up.Normalize();

		// Local variable to reference the direction the camera is facing + 90 clockwise degrees (Which is driven by the Camera target's rotation)
		var right = transform.right;

		// Ensure the camera target doesn't move up and down
		right.z = 0;

		// Normalize the X, Y & Z properties of the right vector to ensure they are between 0 & 1
		right.Normalize();

		// Move the camera (camera_target) Forward relative to current rotation if "W" is pressed or if the mouse moves within the borderWidth distance from the top edge of the screen
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) pos += up * (panSpeed * Time.deltaTime);

		// Move the camera (camera_target) Backward relative to current rotation if "S" is pressed or if the mouse moves within the borderWidth distance from the bottom edge of the screen
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) pos -= up * (panSpeed * Time.deltaTime);

		// Move the camera (camera_target) Right relative to current rotation if "D" is pressed or if the mouse moves within the borderWidth distance from the right edge of the screen
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) pos += right * (panSpeed * Time.deltaTime);

		// Move the camera (camera_target) Left relative to current rotation if "A" is pressed or if the mouse moves within the borderWidth distance from the left edge of the screen
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) pos -= right * (panSpeed * Time.deltaTime);

		ClampXY(ref pos);

		// Setting the camera target's position to the modified pos variable
		transform.position = pos;
	}

	private void Zoom()
	{
		// When we scroll our mouse wheel up, zoom in if the camera is not within the minimum distance (set by our zoomMin variable)
		var scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0.0f)
		{
			targetZoom -= scroll * zoomSpeed;
			targetZoom = Mathf.Clamp(targetZoom, ZoomMin, ZoomMax);
		}

		// smoooooooth scroll like jazz
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, 0.2f);
	}


	private void Drag()
	{
		var pos = transform.position;
		var newMousePos = Input.mousePosition;
		var mouseDelta = newMousePos - lastMousePos;

		var dragSpeed = cam.orthographicSize * 2 / Screen.height;

		if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
			pos -= mouseDelta * dragSpeed;

		ClampXY(ref pos);

		transform.position = pos;
		lastMousePos = newMousePos;
	}

	private void ClampXY(ref Vector3 pos)
	{
		pos.y = Mathf.Clamp(pos.y, -panLimit.y + 0.5f, 0.5f);

		pos.x = Mathf.Clamp(pos.x, 0 - 0.5f, panLimit.x - 0.5f);
	}

	public void CameraUpdate()
	{
		Movement();
		Drag();
		Zoom();
	}
}