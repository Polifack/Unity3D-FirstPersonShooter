using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camera settings
    public bool lockCursor;
    public float mouseSensitivity = 500;
    public Transform target;
    public Vector2 yRotationLimits = new Vector2(-70, 70);
    public float rotationSmoothTime = .12f;
    
    // Use two rotation points to rotate in x axis and y axis separately
    public Transform xAxisRotation;
    public Transform yAxisRotation;

    // Store rotation
    private float currentRotationY;
    private float currentRotationX;


    private void Awake()
    {
        // Initialize counters
        currentRotationX = 0;
        currentRotationY = 0;
    }

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("[Warning]: no target found.");
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        // Rotate camera on X Axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        currentRotationX = Mathf.Lerp(currentRotationX, (currentRotationX - mouseX), rotationSmoothTime);
        xAxisRotation.localRotation = Quaternion.Euler(0, 0, currentRotationX);

        // Rotate camera on Y Axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        currentRotationY = Mathf.Lerp(currentRotationY, (currentRotationY + mouseY), rotationSmoothTime);
        currentRotationY = Mathf.Clamp(currentRotationY, yRotationLimits.x, yRotationLimits.y);
        yAxisRotation.localRotation = Quaternion.Euler(0, currentRotationY, 0);

        // Follow target
        transform.position = target.position;
    }

}
