using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton
    public static CameraController instance;

    // Camera settings
    public float mouseSensitivity = 500;
    public Transform target;
    public Vector2 yRotationLimits = new Vector2(-70, 70);
    public float rotationSmoothTime = .12f;
    public Animator animator;
    
    // Use two rotation points to rotate in x axis and y axis separately
    public Transform xAxisRotation;
    public Transform yAxisRotation;

    // Store rotation
    private float currentRotationY;
    private float currentRotationX;

    // Wobble bool
    private bool isWobbling;
    private bool isWorking = true;

    private void Awake()
    {
        instance = this;
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
    }

    public void setActiveRotation(bool state)
    {
        isWorking = state;
    }

    private void LateUpdate()
    {
        if (!isWorking) return;

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
        if (isWobbling)
        {
            Vector3 nextPosition = target.position;
            nextPosition.z = transform.position.z;
            transform.position = nextPosition;
        }
        else
        {
            transform.position = target.position;
        }
    }

    public void doWobble(bool value)
    {
        isWobbling = value;
        animator.SetBool("isWalking", isWobbling);
    }

}
